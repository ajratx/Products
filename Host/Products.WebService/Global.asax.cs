namespace Products.WebService
{
    using Autofac.Integration.Wcf;
    using System;

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