namespace Products.WebService
{
    using System;
    using System.Web;

    using Autofac.Integration.Wcf;

    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            AutofacHostFactory.Container = Bootstrapper.BuildContainer();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }
    }
}