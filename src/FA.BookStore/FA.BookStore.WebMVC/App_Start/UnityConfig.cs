using FA.BookStore.Data;
using FA.BookStore.Data.Infrastructure;
using FA.BookStore.Data.Infrastructure.Repositories;
using FA.BookStore.Models.Common;
using FA.BookStore.Services;
using FA.BookStore.WebMVC.Areas.Identity.Controllers;
using FA.BookStore.WebMVC.Controllers;
using System;

using Unity;
using Unity.Injection;

namespace FA.BookStore.WebMVC
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterSingleton<BookStoreContext, BookStoreContext>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<RolesAdminController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());
            container.RegisterType<UsersAdminController>(new InjectionConstructor());
            container.RegisterType<IGenericRepository<Category>, GenericRepository<Category>>();
            container.RegisterType<IGenericRepository<Book>, GenericRepository<Book>>();
            container.RegisterType<IGenericRepository<Author>, GenericRepository<Author>>();
            container.RegisterType<IGenericRepository<Review>, GenericRepository<Review>>();
            container.RegisterType<IGenericRepository<Publisher>, GenericRepository<Publisher>>();
            container.RegisterType<ICategoryServices, CategoryServices>();
            container.RegisterType<IBookServices, BookServices>();
            container.RegisterType<IAuthorServices, AuthorServices>();
            container.RegisterType<IReviewServices, ReviewServices>();
            container.RegisterType<IPublisherServices, PublisherServices>();
        }
    }
}