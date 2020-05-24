using App.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Data
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new InMemoryContext(
                serviceProvider.GetRequiredService<DbContextOptions<InMemoryContext>>()))
            {
                // Look for any Movie already in database.
                if (context.Movie.Any())
                {
                    return;   // Database has been seeded
                }

                context.Movie.AddRange(
                    new Movie
                    {
                        Id = 1,
                        Title = "The Lord of the Rings= The Fellowship of the Ring",
                        Director = "Peter Jackson",
                        Price = 100,
                        Rating = 7,
                        Trailer = "http://www.youtube.com",
                        VideoUrl = "http://www.youtube.com",
                        ReleaseDate = new DateTime(2001, 12, 19),
                        Description = "A meek hobbit of the Shire and eight companions set out on a journey to Mount Doom to destroy the One Ring and the dark lord Sauron.",
                        ImdbLink = "http://www.imdb.com/title/tt0120737/",
                        ImageUrl = "http://www.coverwhiz.com/content/The-Lord-Of-The-Rings-The-Fellowship-Of-The-Ring_small.jpg",
                        Actors = new List<Actor>
                        {
                            new Actor { Id= 1 },
                            new Actor { Id= 2 },
                            new Actor { Id= 3 },
                            new Actor { Id= 4 }
                        },
                        Genres = new List<Genre>
                        {
                            new Genre { Id = 1 },
                            new Genre { Id = 2 },
                            new Genre { Id = 3 }
                        }
                    },
                new Movie
                {
                    Id = 2,
                    Title = "The Lord of the Rings= The Two Towers",
                    Director = "Peter Jackson",
                    Price = 100,
                    Rating = 7,
                    Trailer = "http://www.youtube.com",
                    VideoUrl = "http://www.youtube.com",
                    ReleaseDate = new DateTime(2002, 12, 18),
                    Description = "While Frodo and Sam edge closer to Mordor with the help of the shifty Gollum, the divided fellowship makes a stand against Sauron's new ally, Saruman, and his hordes of Isengard.",
                    ImdbLink = "http://www.imdb.com/title/tt0167261/",
                    ImageUrl = "http://www.coverwhiz.com/content/The-Lord-Of-The-Rings-The-Two-Towers_small.jpg",
                    Actors = new List<Actor>
                    {
                        new Actor { Id= 1 },
                        new Actor { Id= 2 },
                        new Actor { Id= 3 },
                        new Actor { Id= 4 },
                    },
                    Genres = new List<Genre>
                    {
                        new Genre { Id = 2 },
                        new Genre { Id = 3 },
                        new Genre { Id = 4 }
                    }
                },
                new Movie
                {
                    Id = 3,
                    Title = "The Lord of the Rings= The Return of the King",
                    Director = "Peter Jackson",
                    Price = 100,
                    Rating = 7,
                    Trailer = "http://www.youtube.com",
                    VideoUrl = "http://www.youtube.com",
                    ReleaseDate = new DateTime(2003, 12, 17),
                    Description = "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.",
                    ImdbLink = "http://www.imdb.com/title/tt0167260/",
                    ImageUrl = "http://www.coverwhiz.com/content/The-Lord-Of-The-Rings-The-Return-Of-The-King_small.jpg",
                    Actors = new List<Actor>
                    {
                        new Actor { Id= 1 },
                        new Actor { Id= 2 },
                        new Actor { Id= 3 },
                        new Actor { Id= 4 },
                    },
                    Genres = new List<Genre>
                    {
                        new Genre { Id = 2 },
                        new Genre { Id = 3 },
                        new Genre { Id = 4 }
                    }
                },
                new Movie
                {
                    Id = 4,
                    Title = "The Point",
                    Director = "Fred Wolf",
                    Price = 100,
                    Rating = 7,
                    Trailer = "http://www.youtube.com",
                    VideoUrl = "http://www.youtube.com",
                    ReleaseDate = new DateTime(1971, 2, 2),
                    Description = "An animated story of an unusual kingdom in which everything and everybody is pointed - except for a young boy named Oblio. Despite his round head, Oblio has many friends. But an evil count, jealous that Oblio is more popular than his own son, says that without a pointed head, Oblio is an outlaw. Along with his faithful dog Arrow, Oblio is exiled to the Pointless Forest. There, he has many fantastic experiences (including encounters with a 3-headed man, giant bees, a tree in the leaf-selling business, and a good-humored old rock). From his adventures, Oblio learns that it is not at all necessary to be pointed to have a point in life. Music composed and performed by Harry Nilsson ('Me and My Arrow'), who also wrote the story. ",
                    ImdbLink = "http://www.imdb.com/title/tt0067595/",
                    Actors = new List<Actor>(),
                    Genres = new List<Genre>
                    {
                        new Genre { Id = 2 },
                        new Genre { Id = 4 },
                        new Genre { Id = 6 }
                    }
                },
                new Movie
                {
                    Id = 5,
                    Title = "Pirates Of The Caribbean= Curse Of The Black Pearl",
                    Director = "Gore Verbinski",
                    Price = 100,
                    Rating = 7,
                    Trailer = "http://www.youtube.com",
                    VideoUrl = "http://www.youtube.com",
                    ReleaseDate = new DateTime(1971, 2, 2),
                    Description = "Blacksmith Will Turner teams up with eccentric pirate 'Captain' Jack Sparrow to save his love, the governor's daughter, from Jack's former pirate allies, who are now undead.",
                    ImdbLink = "http://www.imdb.com/title/tt0325980/",
                    ImageUrl = "http://www.coverwhiz.com/content/Pirates-Of-The-Caribbean-Curse-Of-The-Black-Pearl.jpg",
                    Actors = new List<Actor>
                    {
                        new Actor { Id = 3 },
                        new Actor { Id = 5 },
                        new Actor { Id = 6 }
                    },
                    Genres = new List<Genre>
                    {
                        new Genre { Id = 2 },
                        new Genre { Id = 3 },
                        new Genre { Id = 5 }
                    }
                },
                new Movie
                {
                    Id = 6,
                    Title = "Pirates Of The Caribbean= Dead Man's Chest",
                    Director = "Gore Verbinski",
                    Price = 100,
                    Rating = 7,
                    Trailer = "http://www.youtube.com",
                    VideoUrl = "http://www.youtube.com",
                    ReleaseDate = new DateTime(1971, 2, 2),
                    Description = "Jack Sparrow races to recover the heart of Davy Jones to avoid enslaving his soul to Jones' service, as other friends and foes seek the heart for their own agenda as well.",
                    ImdbLink = "http://www.imdb.com/title/tt0383574/",
                    ImageUrl = "http://www.coverwhiz.com/content/Pirates-Of-The-Caribbean-Dead-Mans-Chest.jpg",
                    Actors = new List<Actor>
                    {
                        new Actor { Id = 3 },
                        new Actor { Id = 5 },
                        new Actor { Id = 6 }
                    },
                    Genres = new List<Genre>
                    {
                        new Genre { Id = 3 },
                        new Genre { Id = 5 },
                        new Genre { Id = 6 }
                    }
                });

                // Look for any Actor already in database.
                if (context.Actor.Any())
                {
                    return;   // Database has been seeded
                }

                context.Actor.AddRange(
                    new Actor
                    {
                        Id = 1,
                        FullName = "Elijah Wood",
                        BirthDate = new DateTime(1981, 01, 28),
                        Country = "USA",
                        ImdbLink = "http://www.imdb.com/name/nm0000704/"
                    },
                new Actor
                {
                    Id = 2,
                    FullName = "Ian McKellen",
                    BirthDate = new DateTime(1939, 5, 25),
                    Country = "UK",
                    ImdbLink = "http://www.imdb.com/name/nm0005212/"
                },
                new Actor
                {
                    Id = 3,
                    FullName = "Orlando Bloom",
                    BirthDate = new DateTime(1981, 1, 13),
                    Country = "UK",
                    ImdbLink = "http://www.imdb.com/name/nm0089217/"
                },
                new Actor
                {
                    Id = 4,
                    FullName = "Viggo Mortensen",
                    BirthDate = new DateTime(1981, 11, 20),
                    Country = "USA",
                    ImdbLink = "http://www.imdb.com/name/nm0001557/"
                },
                new Actor
                {
                    Id = 5,
                    FullName = "Johnny Depp",
                    BirthDate = new DateTime(1981, 6, 9),
                    Country = "USA",
                    ImdbLink = "http://www.imdb.com/name/nm0000136/"
                },
                new Actor
                {
                    Id = 6,
                    FullName = "Keira Knightley",
                    BirthDate = new DateTime(1981, 3, 26),
                    Country = "UK",
                    ImdbLink = "http://www.imdb.com/name/nm0461136/"
                });

                // Look for any Genre already in database.
                if (context.Genre.Any())
                {
                    return;   // Database has been seeded
                }

                context.Genre.AddRange(
                    new Genre { Id = 1, Name = "Drama" },
                    new Genre { Id = 2, Name = "Adventure" },
                    new Genre { Id = 3, Name = "Action" },
                    new Genre { Id = 4, Name = "Fantasy" },
                    new Genre { Id = 5, Name = "Animation" },
                    new Genre { Id = 6, Name = "Family" }
                    );

                // Look for any User already in database.
                if (context.User.Any())
                {
                    return;   // Database has been seeded
                }

                context.User.AddRange(
                    new User { Id = 1, Email = "admin@admin.com", Password = "a.A123" },
                    new User { Id = 2, Email = "user@user.com", Password = "a.A123" }
                );

                context.SaveChanges();
            }
        }
    }
}
