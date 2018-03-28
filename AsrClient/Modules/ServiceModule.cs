//#define MOQ

namespace Web.Modules
{
    using Autofac;
    using Domain.Services;
    using Domain.Services.Recognizer;
    using Domain.Services.Speaker.Implementations;
    using DomainTest.Services.Recognizer;

    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(SpeakerService).Assembly)
                .AssignableTo<IDomainService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

#if MOQ
            builder
                .RegisterType<MoqRecognizer>()
                .As<IRecognizerService>()
                .InstancePerLifetimeScope();
#endif
        }
    }
}