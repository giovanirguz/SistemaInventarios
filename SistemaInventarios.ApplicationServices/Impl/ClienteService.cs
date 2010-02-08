using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Data.NHibernate;
using SistemaInventarios.Model;

namespace SistemaInventarios.ApplicationServices.Impl
{
    public class ClienteService : IClienteService
    {
        readonly IRepository<Cliente> clienteRepository;

        public ClienteService(IRepository<Cliente> clienteRepository)
        {
            this.clienteRepository = clienteRepository;
        }

        protected virtual ISession Session
        {
            get
            {
                string factoryKey = SessionFactoryAttribute.GetKeyFrom(this);
                return NHibernateSession.CurrentFor(factoryKey);
            }
        }

        public Cliente GetClienteById(int id)
        {
            return clienteRepository.Get(id);
        }

        public void SaveCliente(Cliente cliente)
        {
            clienteRepository.SaveOrUpdate(cliente);
            clienteRepository.DbContext.CommitChanges();
        }

        public Cliente[] GetClientesByName(string name)
        {
            string[] values = name.Split(' ');

            var criteria = DetachedCriteria.For(typeof (Cliente))
                .SetMaxResults(20)
                .SetProjection(Projections.ProjectionList()
                                   .Add(Projections.Property("ApellidoPaterno"), "ApellidoPaterno")
                                   .Add(Projections.Property("ApellidoMaterno"), "ApellidoMaterno")
                                   .Add(Projections.Property("Nombre"), "Nombre")
                                   .Add(Projections.Property("NoControl"), "NoControl")
                                   .Add(Projections.Property("Rfc"), "Rfc")
                )
                .AddOrder(Order.Asc("ApellidoPaterno"))
                .AddOrder(Order.Asc("ApellidoMaterno"))
                .AddOrder(Order.Asc("Nombre"))
                .SetResultTransformer(NHibernate.Transform.Transformers.AliasToBean(typeof(ClienteDTO)));

            if (values.Length == 1)
                criteria.Add(Expression.Or(Expression.Or(Expression.Like("ApellidoPaterno", values[0] + "%"),
                                                         Expression.Like("ApellidoMaterno", values[0] + "%")),
                                           Expression.Like("Nombre", values[0] + "%")));
            else if (values.Length == 2)
                criteria.Add(Expression.Or(Expression.Or(Expression.Like("ApellidoPaterno", values[0] + "%"),
                                                         Expression.Like("ApellidoMaterno", values[0] + "%")),
                                           Expression.Like("Nombre", values[1] + "%")));
            else if (values.Length == 3)
                criteria.Add(Expression.Or(Expression.Or(Expression.Like("ApellidoPaterno", values[0] + "%"),
                                                         Expression.Like("ApellidoMaterno", values[1] + "%")),
                                           Expression.Like("Nombre", values[2] + "%")));
            else if (values.Length > 3)
            {
                var nombre = String.Empty;
                for (var i = 2; i < values.Length; i++)
                    nombre += name[i] + " ";

                criteria.Add(Expression.Or(Expression.Or(Expression.Like("ApellidoPaterno", values[0] + "%"),
                                                         Expression.Like("ApellidoMaterno", values[1] + "%")),
                                           Expression.Like("Nombre", nombre.Trim() + "%")));
            }

            var list = criteria.GetExecutableCriteria(Session).List<ClienteDTO>();

            var result = new List<Cliente>();
            foreach (var item in list)
            {
                result.Add(new Cliente {NoControl = item.NoControl, Nombre = item.NombreCompleto(), Rfc = item.Rfc});
            }

            return result.ToArray();
        }

        public Cliente GetClienteByNoControl(int noControl)
        {
            return clienteRepository.FindOne(new Dictionary<string, object> {{"NoControl", noControl}});
        }

        class ClienteDTO
        {
            public int Id { get; set; }
            public string ApellidoPaterno { get; set; }
            public string ApellidoMaterno { get; set; }
            public string Nombre { get; set; }
            public int NoControl { get; set; }
            public string Rfc { get; set; }

            public string NombreCompleto()
            {
                return String.Format("{0} {1} {2}", Nombre, ApellidoPaterno, ApellidoMaterno).Trim();
            }
        }
    }
}