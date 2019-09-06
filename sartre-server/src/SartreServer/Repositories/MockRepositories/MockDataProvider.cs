using SartreServer.Models;
using System.Collections.Generic;

namespace SartreServer.Repositories.MockRepositories
{
    public class MockDataProvider
    {
        public Dictionary<string, User> Users { get; }

        public Dictionary<string, Blog> Blogs { get; }

        public Dictionary<string, Post> Posts { get; }

        public MockDataProvider()
        {
            Users = new Dictionary<string, User>();
            Blogs = new Dictionary<string, Blog>();
            Posts = new Dictionary<string, Post>();

            User alex = new User()
            {
                Login = "alex",
                Password = "test",
                Name = "Alexandre",
                Roles = new Role[] { Role.Administrator, Role.Author },
                Biography = "I am the admin!",
                Website = "www.github.com"
            };
            User anna = new User()
            {
                Login = "anna",
                Password = "test",
                Name = "Anna",
                Roles = new Role[] { Role.Author },
                Biography = "I am some random blog author!",
                Website = "www.github.com"
            };
            User tobi = new User()
            {
                Login = "tobi",
                Password = "test",
                Name = "Tobias",
                Roles = new Role[] { Role.Author },
                Biography = "Rust programmer of doom!",
                Website = "www.github.com"
            };

            Users.Add(alex.Login, alex);
            Users.Add(anna.Login, anna);
            Users.Add(tobi.Login, tobi);

            Blog mainBlog = new Blog()
            {
                Id = "main",
                Title = "Main Blog",
                Description = "This is the main blog of this Sartre installation.",
                Contributors = new User[] { alex, tobi }
            };
            Blog fashionBlog = new Blog()
            {
                Id = "fashion",
                Title = "Anna's Fresh Fashion Blog",
                Description = "A fresh take on seasonal fashion trends.",
                Contributors = new User[] { anna }
            };

            Blogs.Add(mainBlog.Id, mainBlog);
            Blogs.Add(fashionBlog.Id, fashionBlog);

            for (int i = 0; i < 50; i++)
            {
                string id = $"post-{i}";
                if (i < 20)
                {
                    IEnumerable<User> authors = null;
                    if (i < 10)
                    {
                        authors = new User[] { alex };
                    }
                    else
                    {
                        authors = new User[] { alex, tobi };
                    }
                    Posts.Add(id, new Post()
                    {
                        Id = id,
                        Title = id,
                        Blog = mainBlog,
                        Published = true,
                        Authors = authors,
                        Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras accumsan lorem magna, quis imperdiet justo fermentum eu. Duis at euismod turpis. Integer facilisis pellentesque egestas. Sed aliquet, purus et laoreet ullamcorper, lectus neque tincidunt eros, eu lacinia ante neque ac quam. Cras imperdiet magna hendrerit aliquam ultricies. Nullam sollicitudin lacus at massa volutpat, sed condimentum massa tristique. Nam hendrerit a velit ac tempus. Morbi a massa nisl. Praesent a diam dolor. Fusce luctus enim non turpis suscipit, non viverra risus facilisis. Quisque tempor pellentesque orci, a scelerisque lorem consectetur eu. "
                    });
                }
                else
                {
                    Posts.Add(id, new Post()
                    {
                        Id = id,
                        Title = id,
                        Blog = fashionBlog,
                        Published = true,
                        Authors = new User[] { anna },
                        Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras accumsan lorem magna, quis imperdiet justo fermentum eu. Duis at euismod turpis. Integer facilisis pellentesque egestas. Sed aliquet, purus et laoreet ullamcorper, lectus neque tincidunt eros, eu lacinia ante neque ac quam. Cras imperdiet magna hendrerit aliquam ultricies. Nullam sollicitudin lacus at massa volutpat, sed condimentum massa tristique. Nam hendrerit a velit ac tempus. Morbi a massa nisl. Praesent a diam dolor. Fusce luctus enim non turpis suscipit, non viverra risus facilisis. Quisque tempor pellentesque orci, a scelerisque lorem consectetur eu. "
                    });
                }
            }
        }


    }
}
