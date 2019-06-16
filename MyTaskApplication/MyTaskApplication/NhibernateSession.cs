using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTaskApplication
{
    //Define Nhibernate session
    public class NhibernateSession
    {
        public static ISession OpenSession()
        {
            var configuration = new Configuration();
            var configurationPath = HttpContext.Current.Server.MapPath(@"~\Models\Nhibernate.cfg.xml");
            configuration.Configure(configurationPath);
            var DocumentsConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\Documents.hbm.xml");
            configuration.AddFile(DocumentsConfigurationFile);
            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}