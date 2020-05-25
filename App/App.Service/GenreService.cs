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

namespace App.Service
{
    public interface IGenreService
    {
        Task<ServiceResponse<Genre>> Get(int id);
        Task<ServiceResponse<List<Genre>>> Get(GenreFilter request);
        Task<ServiceResponse<Genre>> Insert(Genre Genre);
        ServiceResponse<Genre> Update(Genre Genre);
        ServiceResponse<bool> Delete(int id);
    }

    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre> _genreRepo;
        public GenreService(
            IRepository<Genre> genreRepo
            )
        {
            _genreRepo = genreRepo;
        }

        public async Task<ServiceResponse<Genre>> Get(int id)
        {
            var genre = await _genreRepo.Table
                .FirstOrDefaultAsync(x => x.Id == id);
            return new ServiceResponse<Genre>(genre, true);
        }

        public async Task<ServiceResponse<List<Genre>>> Get(GenreFilter model)
        {
            ExpressionStarter<Genre> expression = PredicateBuilder.New<Genre>(true);
            if (model.Name.Length > 0)
                expression = expression.And(prop => model.Name.Contains(prop.Name));
            var responseD = _genreRepo.Pagining(expression, ((model.PageNumber - 1) * model.PageSize), model.PageSize, model.OrderColumn, (OrderDirection)model.OrderBy, true);
            var response = await responseD.ToListAsync();
            return new ServiceResponse<List<Genre>>(response, true);
        }

        public ServiceResponse<Genre> Update(Genre Genre)
        {
            var nGenre = _genreRepo.Get(x => x.Id == Genre.Id);
            if (nGenre == null)
                return new ServiceResponse<Genre>(false, "Genre doesnt exist");
            _genreRepo.Update(Genre);
            return new ServiceResponse<Genre>(Genre, true);
        }

        public async Task<ServiceResponse<Genre>> Insert(Genre genre)
        {
            var nGenre = _genreRepo.Table.FirstOrDefault(x => x.Name == genre.Name);
            if (nGenre != null) return new ServiceResponse<Genre>(false, "GenreExist");
            var newId = _genreRepo.Table.Select(x => x.Id).Max() + 1;
            genre.Id = newId;
            await _genreRepo.Insert(genre);
            return new ServiceResponse<Genre>(genre, true);
        }

        public ServiceResponse<bool> Delete(int id)
        {
            var nGenre = _genreRepo.Get(x => x.Id == id);
            if (nGenre == null)
                return new ServiceResponse<bool>(false, "GenreDoesntExist");
            _genreRepo.Delete(nGenre);
            return new ServiceResponse<bool>(true, "Deleted");
        }

    }
}
