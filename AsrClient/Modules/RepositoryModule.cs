namespace Web.Modules
{
    using Autofac;
    using Domain.Repoository;
    using Infrastructure.Repository;
    using Microsoft.EntityFrameworkCore;

    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterGeneric(typeof(EntityFrameworkCoreRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            builder
                .RegisterInstance(new ApplicationContext())
                .As(typeof(DbContext));
        }
    }
}