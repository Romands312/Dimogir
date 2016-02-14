using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dimogir.Services;
using Ninject;
using Ninject.Web.Common;

namespace Dimogir.Web.Utils
{
    public class BlubbModule : GlobalKernelRegistrationModule<OnePerRequestHttpModule>
    {
    }

    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Load(new BlubbModule());
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<ICategoryService>().To<CategoryService>();
            kernel.Bind<ILessonService>().To<LessonService>();
        }
    }
}