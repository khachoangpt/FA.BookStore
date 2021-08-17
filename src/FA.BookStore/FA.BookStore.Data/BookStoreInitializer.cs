using FA.BookStore.Models.Common;
using FA.BookStore.Models.Securiry;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FA.BookStore.Data
{
    public class BookStoreInitializer : DropCreateDatabaseIfModelChanges<BookStoreContext>
    {
        protected override void Seed(BookStoreContext context)
        {
            InitializeIdentity(context);

            var categories = new List<Category>()
            {
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Văn Học",
                    Description = "Văn Học"
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Lịch Sử",
                    Description = "Lịch Sử"
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Truyện Tranh",
                    Description = "Truyện Tranh"
                },
            };

            var publishers = new List<Publisher>()
            {
                new Publisher()
                {
                    Id = Guid.NewGuid(),
                    Name = "Nhà xuất bản Hà Nội",
                    Description = "Nhà xuất bản Hà Nội"
                },
                new Publisher()
                {
                    Id = Guid.NewGuid(),
                    Name = "Nhà xuất bản Văn Học",
                    Description = "Nhà xuất bản Văn Học"
                },
                new Publisher()
                {
                    Id = Guid.NewGuid(),
                    Name = "Nhà xuất bản Giáo Dục",
                    Description = "Nhà xuất bản Giáo Dục"
                },
                new Publisher()
                {
                    Id = Guid.NewGuid(),
                    Name = "Nhà xuất bản Phụ nữ",
                    Description = "Nhà xuất bản Phụ nữ"
                }
            };

            var author1 = new Author()
            {
                Id = Guid.NewGuid(),
                Name = "Sharon Salzberg",
                Description = "Sharon Salzberg"
            };
            var author2 = new Author()
            {
                Id = Guid.NewGuid(),
                Name = "Thiện Từ",
                Description = "Thiện Từ"
            };
            var author3 = new Author()
            {
                Id = Guid.NewGuid(),
                Name = "Như Nhiên Thích Tánh Tuệ",
                Description = "Như Nhiên Thích Tánh Tuệ"
            };

            var books = new List<Book>()
            {
                new Book()
                {
                    Id = Guid.NewGuid(),
                    Title = "Con Chim Xanh Biếc Bay Về",
                    Summary = "Không giống như những tác phẩm trước đây lấy bối cảnh vùng quê miền Trung đầy ắp những hoài niệm tuổi thơ dung dị, trong trẻo với các nhân vật ở độ tuổi dậy thì, trong quyển sách mới lần này nhà văn Nguyễn Nhật Ánh lấy bối cảnh chính là Sài Gòn – Thành phố Hồ Chí Minh nơi tác giả sinh sống (như là một sự đền đáp ân tình với mảnh đất miền Nam). Các nhân vật chính trong truyện cũng “lớn” hơn, với những câu chuyện mưu sinh lập nghiệp lắm gian nan thử thách của các sinh viên trẻ đầy hoài bão. Tất nhiên không thể thiếu những câu chuyện tình cảm động, kịch tính và bất ngờ khiến bạn đọc ngẩn ngơ, cười ra nước mắt. Và như trong mọi tác phẩm Nguyễn Nhật Ánh, sự tử tế và tinh thần hướng thượng vẫn là điểm nhấn quan trọng trong quyển sách mới này.",
                    ImgUrl = "book.jpg",
                    UnitPrice = 202500M,
                    Quantity = 12,
                    CreatedDate = new DateTime(2020,02,03),
                    UpdatedDate = new DateTime(2020,05,12),
                    Published = true,
                    Category = categories.Single(x => x.Name == categories[0].Name),
                    Publisher = publishers.Single(x => x.Name == publishers[0].Name),
                    Authors = new List<Author>{ author1, author3 }
                },
                new Book()
                {
                    Id = Guid.NewGuid(),
                    Title = "Bánh Mì Cô Đơn",
                    Summary = "Cuốn tiểu thuyết Bánh Mì Cô Đơn của tác giả Judith Ryan Hendricks viết về những thân phận phụ nữ hết sức đời thương. Là Wyn Morrison, biết rất nhiều nhưng không rõ mình muốn gì? Là Christine Mayle, yêu sàn diễn đến cháy bỏng, nhưng không dễ gì tìm được người đàn ông của đời mình. Là người mẹ hoàn hảo của Wyn, sau nhiều năm trời im lặng đã mở ra những tâm sự chân thành cùng con gái. Là những phụ nữ vừa bình dị vừa độc đáo ở hiệu bánh Quen Srteet. Tất cả ít nhiều đã giúp Wyn nhìn lại chính mình sau khi mải sống cuộc đời nhung lụa của một cô vợ được cưới về chỉ cốt để làm sang cho chồng.",
                    ImgUrl = "book1.jpg",
                    UnitPrice = 96750M,
                    Quantity = 25,
                    CreatedDate = new DateTime(2020,04,15),
                    UpdatedDate = new DateTime(2020,05,22),
                    Published = false,
                    Category = categories.Single(x => x.Name == categories[1].Name),
                    Publisher = publishers.Single(x => x.Name == publishers[1].Name),
                    Authors = new List<Author>{ author2, author3 }
                },
                new Book()
                {
                    Id = Guid.NewGuid(),
                    Title = "Hạnh Phúc Đích Thực",
                    Summary = "Con người tìm đến thiền định vì họ muốn bản thân có thể đưa ra những quyết định đúng đắn, xóa bỏ những thói quen không tốt và hồi phục tốt hơn sau những thất vọng. Họ muốn cảm thấy gần gũi hơn với gia đình, bạn bè; muốn thoải mái dễ chịu hơn với chính cơ thể và tâm trí mình; hoặc trở thành một phần của điều gì đó lớn lao hơn bản thân họ.",
                    ImgUrl = "book2.jpg",
                    UnitPrice = 70400M,
                    Quantity = 44,
                    CreatedDate = new DateTime(2020,10,20),
                    UpdatedDate = new DateTime(2021,01,12),
                    Published = true,
                    Category = categories.Single(x => x.Name == categories[2].Name),
                    Publisher = publishers.Single(x => x.Name == publishers[2].Name),
                    Authors = new List<Author>{ author1, author2, author3 }
                },
                new Book()
                {
                    Id = Guid.NewGuid(),
                    Title = "Hạnh Phúc Không Nằm Trong Ví",
                    Summary = "Trong cuộc sống hằng ngày, người ta thường hay đùa với nhau: “Tiền là tiên, là Phật.” Có lẽ không ngoa khi nói rằng cuộc sống hiện nay là cuộc sống của tiền bạc. Đa số mọi người đều coi tiền bạc là trọng tâm của đời mình, sống để kiếm tiền. Vui ở tiền bạc mà buồn cũng ở tiền bạc. Có tiền là có tất cả mà mất tiền là mất tất cả. Nhưng liệu có đúng như vậy không?",
                    ImgUrl = "book3.jpg",
                    UnitPrice = 50150M,
                    Quantity = 44,
                    CreatedDate = new DateTime(2020,10,20),
                    UpdatedDate = new DateTime(2021,01,12),
                    Published = true,
                    Category = categories.Single(x => x.Name == categories[0].Name),
                    Publisher = publishers.Single(x => x.Name == publishers[0].Name),
                    Authors = new List<Author>{ author1, author2, author3 }
                },                
                new Book()
                {
                    Id = Guid.NewGuid(),
                    Title = "Hạnh Phúc Thật Gần",
                    Summary = "Hãy lật dở từng trang sách thật nhẹ nhàng để cảm nhận từng nhịp thở của sự hạnh phúc đang ở bên ta. Rồi một lúc nào đó bạn chợt nhận ra hạnh phúc chẳng đâu xa, chỉ đơn giản là những ánh nhìn cảm thông, thấu hiểu cũng đủ để trái tim ấm lại, tâm hồn sẽ cảm thấy nhẹ nhàng, thanh tịnh và sẵn sàng đón nhận những điều tuyệt vời mà cuộc sống này mang lại.",
                    ImgUrl = "book4.jpg",
                    UnitPrice = 53300M,
                    Quantity = 44,
                    CreatedDate = new DateTime(2020,10,20),
                    UpdatedDate = new DateTime(2021,01,12),
                    Published = true,
                    Category = categories.Single(x => x.Name == categories[1].Name),
                    Publisher = publishers.Single(x => x.Name == publishers[1].Name),
                    Authors = new List<Author>{ author1, author2, author3 }
                },
                new Book()
                {
                    Id = Guid.NewGuid(),
                    Title = "Nhẹ Gánh Ưu Phiền",
                    Summary = "Sự thiện lương và bình an trong nội tâm của bạn sẽ góp phần tạo nên sự bình an của thế giới bên ngoài. Và sự bình an chung ấy sẽ ảnh hưởng đến nhiều người khác quanh bạn. Thế nên, ngay giây phút này, bạn hãy dám sống như một bông hoa, hồn nhiên nở, hết lòng dâng hiến vẻ đẹp và hương thơm cho đời.",
                    ImgUrl = "book5.jpg",
                    UnitPrice = 76440M,
                    Quantity = 44,
                    CreatedDate = new DateTime(2020,10,20),
                    UpdatedDate = new DateTime(2021,01,12),
                    Published = true,
                    Category = categories.Single(x => x.Name == categories[2].Name),
                    Publisher = publishers.Single(x => x.Name == publishers[2].Name),
                    Authors = new List<Author>{ author1, author2, author3 }
                },
                new Book()
                {
                    Id = Guid.NewGuid(),
                    Title = "Bão Giông Mới Là Cuộc Đời",
                    Summary = "Chúng ta có luôn hài lòng với cuộc sống của mình không? Chư vị thánh hiền trong quá khứ dạy rằng nếu chia đời mình thành mười phần bằng nhau, ta thường chỉ thấy hạnh phúc một hoặc hai phần. Đức Phật cũng luôn dạy rằng đời là bể khổ. Dẫu không phải là đau khổ của sinh lão bệnh tử thì cũng không tránh được nỗi đau khi xa lìa người thân yêu, đối mặt với kẻ thù và không đạt được những điều mong muốn.",
                    ImgUrl = "book6.jpg",
                    UnitPrice = 87200M,
                    Quantity = 44,
                    CreatedDate = new DateTime(2020,10,20),
                    UpdatedDate = new DateTime(2021,01,12),
                    Published = true,
                    Category = categories.Single(x => x.Name == categories[0].Name),
                    Publisher = publishers.Single(x => x.Name == publishers[0].Name),
                    Authors = new List<Author>{ author1, author2, author3 }
                },
                new Book()
                {
                    Id = Guid.NewGuid(),
                    Title = "Dám Tha Thứ",
                    Summary = "Dám Tha Thứ là một tác phẩm có sức lay động và ảnh hưởng mạnh mẽ đến cái nhìn của chúng ta về sự tha thứ. Bằng những kinh nghiệm thực tế của bản thân trong lĩnh vực điều trị tâm lý, bằng chính những đau khổ, mất mát của cuộc đời mình, tác giả đã đúc kết rằng: “Tha thứ có sức mạnh kết nối con người với nhau. Ngọn lửa hận thù từng ngự trị trong trái tim mỗi chúng ta đều có thể được thay thế bằng một tình yêu thương ấm áp”.",
                    ImgUrl = "book7.jpg",
                    UnitPrice = 56000M,
                    Quantity = 44,
                    CreatedDate = new DateTime(2020,10,20),
                    UpdatedDate = new DateTime(2021,01,12),
                    Published = true,
                    Category = categories.Single(x => x.Name == categories[1].Name),
                    Publisher = publishers.Single(x => x.Name == publishers[1].Name),
                    Authors = new List<Author>{ author1, author2, author3 }
                },
                new Book()
                {
                    Id = Guid.NewGuid(),
                    Title = "Ngày Mới Tự Làm Mới",
                    Summary = "Những ngày cuối năm và nhất là khi cả dân tộc Việt Nam cùng chào đón năm mới âm lịch Kỷ Hợi 2019, rất nhiều Phật tử đã thỉnh và tặng nhau lịch thư pháp NĂM MỚI, TA CŨNG MỚI của Thiền sư Thích Nhất Hạnh. Một món quà đơn giản nhưng ý nghĩa vô cùng.",
                    ImgUrl = "book8.jpg",
                    UnitPrice = 48300M,
                    Quantity = 44,
                    CreatedDate = new DateTime(2020,10,20),
                    UpdatedDate = new DateTime(2021,01,12),
                    Published = true,
                    Category = categories.Single(x => x.Name == categories[2].Name),
                    Publisher = publishers.Single(x => x.Name == publishers[2].Name),
                    Authors = new List<Author>{ author1, author2, author3 }
                },
                new Book()
                {
                    Id = Guid.NewGuid(),
                    Title = "Muốn An Được An",
                    Summary = "Ngày chủ nhật và cũng là ngày cuối cùng của tháng 11 năm 2014 tôi nhận được bản thảo cuốn sách Muốn an được an của thiền sư Thích Nhất Hạnh đã được sư cô Hội Nghiêm dịch ra tiếng Việt từ bản nguyên gốc tiếng anh Being peace. Tôi ngồi vào bàn rồi đọc ngay tức khắc. Và tôi giật mình.",
                    ImgUrl = "book9.jpg",
                    UnitPrice = 41600M,
                    Quantity = 44,
                    CreatedDate = new DateTime(2020,10,20),
                    UpdatedDate = new DateTime(2021,01,12),
                    Published = true,
                    Category = categories.Single(x => x.Name == categories[0].Name),
                    Publisher = publishers.Single(x => x.Name == publishers[0].Name),
                    Authors = new List<Author>{ author1, author2, author3 }
                },
            };

            var Reviews = new List<Review>()
            {
                new Review()
                {
                    Id = Guid.NewGuid(),
                    Book = books.Single(x => x.Title == books[0].Title),
                    Content = "Content 01",
                    CreatedDate = new DateTime(2021,05,10)
                },
                new Review()
                {
                    Id = Guid.NewGuid(),
                    Book = books.Single(x => x.Title == books[1].Title),
                    Content = "Content 02",
                    CreatedDate = new DateTime(2021,05,10)
                },
                new Review()
                {
                    Id = Guid.NewGuid(),
                    Book = books.Single(x => x.Title == books[2].Title),
                    Content = "Content 03",
                    CreatedDate = new DateTime(2021,05,10)
                },
                new Review()
                {
                    Id = Guid.NewGuid(),
                    Book = books.Single(x => x.Title == books[0].Title),
                    Content = "Content 04",
                    CreatedDate = new DateTime(2021,05,10)
                }
            };

            context.Categories.AddRange(categories);
            context.Publishers.AddRange(publishers);
            context.Books.AddRange(books);
            context.Reviews.AddRange(Reviews);
            context.SaveChanges();
        }

        public static void InitializeIdentity(BookStoreContext db)
        {
            var userManager = new UserManager<User>(new UserStore<User>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            const string name = "admin@example.com";
            const string password = "Admin@123456";
            const string roleName = "Admin";

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleResult = roleManager.Create(role);
            }

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new User { UserName = name, Email = name };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }
    }
}
