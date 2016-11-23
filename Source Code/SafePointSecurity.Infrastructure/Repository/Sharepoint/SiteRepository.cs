using SafePointSecurity.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;

namespace SafePointSecurity.Infrastructure.Repository.Sharepoint
{
    public class SiteRepository : ISiteRepository
    {
        public List<Model.Site> FindAll(string connectionString = null)
        {
            throw new NotImplementedException();
        }

        public bool Save(Model.Site entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Model.Site entity)
        {
            throw new NotImplementedException();
        }

        public bool AdicionarPermissao(string urlSite, string nomeGrupo, string nomePermissao)
        {
            var retorno = false;
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPWeb web = new SPSite(SPContext.Current.Site.ID).OpenWeb(String.Concat(@"/", urlSite)))
                {
                    var anterior = web.AllowUnsafeUpdates;
                    web.AllowUnsafeUpdates = true;

                    //Criar grupo:
                    SPGroup group = null;
                    try
                    {
                        group = web.SiteGroups[nomeGrupo];
                    }
                    catch (Exception)
                    {
                        if (group == null)
                        {
                            web.SiteGroups.Add(nomeGrupo, SPContext.Current.Web.CurrentUser, web.Author, "Descrição do Grupo");
                            group = web.SiteGroups[nomeGrupo];
                        }
                    }

                    // Adicionar permissão:
                    SPRoleType type = SPRoleType.None;
                    switch (nomePermissao)
                    {
                        case "Contribuição": type = SPRoleType.Contributor; break;
                        case "Controle Total": type = SPRoleType.Administrator; break;
                        case "Leitura": type = SPRoleType.Reader; break;
                        case "Edição": type = SPRoleType.Editor; break;
                        case "Designer": type = SPRoleType.WebDesigner; break;
                    }
                    SPRoleDefinition roleDefinition = web.RoleDefinitions.GetByType(type);

                    SPRoleAssignment roleAssignment = new SPRoleAssignment(group);
                    roleAssignment.RoleDefinitionBindings.Add(roleDefinition);
                    web.RoleAssignments.Add(roleAssignment);
                    web.Update();
                    web.AllowUnsafeUpdates = !anterior;
                    retorno = true;
                }
            });

            return retorno;
        }

        public bool RemoverPermissao(string urlSite, string nomeGrupo)
        {
            var retorno = false;

            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPWeb web = new SPSite(SPContext.Current.Site.ID).OpenWeb(String.Concat(@"/", urlSite)))
                {
                    var anterior = web.AllowUnsafeUpdates;
                    web.AllowUnsafeUpdates = true;

                    try
                    {
                        web.RoleAssignments.Remove(web.SiteGroups[nomeGrupo]);
                    }
                    catch (Exception)
                    {
                        web.RoleAssignments.Remove(web.EnsureUser(nomeGrupo));
                    }

                    web.Update();
                    web.AllowUnsafeUpdates = !anterior;
                    retorno = true;
                }
            });

            return retorno;
        }
    }
}
