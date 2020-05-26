using App.Core.Aspects.Authorization;
using App.Core.Aspects.Caching;
using App.Core.DbTrackers;
using App.Core.Response;
using App.Data.Models;
using App.Entity.Enum;
using App.Entity.Filters;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Service.Pack
{
    public interface IActorService
    {
        Task<ServiceResponse<Actor>> Get(int id);
        Task<ServiceResponse<List<Actor>>> Get(ActorFilter request);
        Task<ServiceResponse<Actor>> Insert(Actor actor);
        ServiceResponse<Actor> Update(Actor actor);
        ServiceResponse<bool> Delete(int id);
    }

    public class ActorService : IActorService
    {
        private readonly IRepository<Actor> _actorRepo;
        public ActorService(
            IRepository<Actor> actorRepo
            )
        {
            _actorRepo = actorRepo;
        }

        public async Task<ServiceResponse<Actor>> Get(int id)
        {
            var actor = await _actorRepo.Table
                .FirstOrDefaultAsync(x => x.Id == id);
            return new ServiceResponse<Actor>(actor, true);
        }

        public async Task<ServiceResponse<List<Actor>>> Get(ActorFilter model)
        {
            ExpressionStarter<Actor> expression = PredicateBuilder.New<Actor>(true);
            if (model.Name.Length > 0)
                expression = expression.And(prop => model.Name.Contains(prop.FullName));
            var responseD = _actorRepo.Pagining(expression, ((model.PageNumber - 1) * model.PageSize), model.PageSize, model.OrderColumn, (OrderDirection)model.OrderBy, true);
            var response = await responseD.ToListAsync();
            return new ServiceResponse<List<Actor>>(response, true);
        }

        public ServiceResponse<Actor> Update(Actor actor)
        {
            var nactor = _actorRepo.Get(x => x.Id == actor.Id);
            if (nactor == null)
                return new ServiceResponse<Actor>(false, "actor doesnt exist");
            _actorRepo.Update(actor);
            return new ServiceResponse<Actor>(actor, true);
        }

        [AuthorizationAspect("IUserManager.AddAsync")]
        [CacheRemoveAspect("IUserManager.Get")]
        public async Task<ServiceResponse<Actor>> Insert(Actor actor)
        {
            var nactor = _actorRepo.Table.FirstOrDefault(x => x.FullName == actor.FullName);
            if (nactor != null) return new ServiceResponse<Actor>(false, "actorExist");
            var newId = _actorRepo.Table.Select(x => x.Id).Max() + 1;
            actor.Id = newId;
            await _actorRepo.Insert(actor);
            return new ServiceResponse<Actor>(actor, true);
        }

        public ServiceResponse<bool> Delete(int id)
        {
            var nactor = _actorRepo.Get(x => x.Id == id);
            if (nactor == null)
                return new ServiceResponse<bool>(false, "actorDoesntExist");
            _actorRepo.Delete(nactor);
            return new ServiceResponse<bool>(true, "Deleted");
        }

    }
}
