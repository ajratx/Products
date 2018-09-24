namespace Products.WebService
{
    using System;

    using Autofac.Integration.Wcf;

    public class Global : System.Web.HttpApplication
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