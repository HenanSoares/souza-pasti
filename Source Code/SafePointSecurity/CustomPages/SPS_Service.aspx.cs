using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.WebPartPages;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using Microsoft.SharePoint;
using SafePointSecurity.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SafePointSecurity.Application.Interfaces;
using SafePointSecurity.Application.Facade;
using SafePointSecurity.Common.Util;
using SafePointSecurity.ViewModel;

namespace SafePointSecurity
{
    public class SPS_Service : WebPartPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static dynamic TesteConnection()
        {
            return String.Concat("Conexão realizada com sucesso. ", "Usuário: ", SPContext.Current.Web.CurrentUser.Name);
        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static dynamic GetPermissoes()
        {
            try
            {
                if (VisualWebPart1._editMode)
                    return new List<Permissao>();

                Connection connection = Connection.Instance;
                connection.ConnectionString = VisualWebPart1._connectionString;
                connection.User = VisualWebPart1._username;
                connection.Password = VisualWebPart1._password;

                IApplicationFacade application = new ApplicationFacade();
                var list = new List<Permissao>();

                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    list = application.GetPermissoes(connection);
                });

                return JsonConvert.SerializeObject(
                    list, new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static dynamic GetSitesHierarquia()
        {
            try
            {
                Connection connection = Connection.Instance;
                connection.ConnectionString = VisualWebPart1._connectionString;
                connection.User = VisualWebPart1._username;
                connection.Password = VisualWebPart1._password;

                IApplicationFacade application = new ApplicationFacade();
                var list = new List<Site>();

                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    list = application.GetSites(connection);
                });

                return JsonConvert.SerializeObject(
                   list, new JsonSerializerSettings
                   {
                       ContractResolver = new CamelCasePropertyNamesContractResolver()
                   });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string AddPermissao(PermissaoViewModel viewModel)
        {
            try
            {
                #region Connection
                Connection connection = Connection.Instance;
                connection.ConnectionString = VisualWebPart1._connectionString;
                connection.User = VisualWebPart1._username;
                connection.Password = VisualWebPart1._password;
                #endregion

                //IApplicationFacade application = new ApplicationFacade();
                //var list = new List<Site>();

                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPWeb web = new SPSite(SPContext.Current.Site.ID).OpenWeb(String.Concat(@"/", viewModel.site)))
                    {
                        var anterior = web.AllowUnsafeUpdates;
                        web.AllowUnsafeUpdates = true;

                        //Criar grupo:
                        SPGroup group = null;                        
                        try
                        {
                            group = web.SiteGroups[viewModel.nome];
                        }
                        catch (Exception)
                        {
                            if (group == null)
                            {
                                web.SiteGroups.Add(viewModel.nome, SPContext.Current.Web.CurrentUser, web.Author, "Your Group Description");
                                group = web.SiteGroups[viewModel.nome];
                            }
                        }


                        // Adicionar permissão:
                        SPRoleType type = SPRoleType.None;
                        switch (viewModel.nivelPermissao)
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
                    }
                });


                return JsonConvert.SerializeObject(
                   viewModel, new JsonSerializerSettings
                   {
                       ContractResolver = new CamelCasePropertyNamesContractResolver()
                   });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static dynamic RemoverPermissao(PermissaoViewModel viewModel)
        {
            try
            {
                #region Connection
                Connection connection = Connection.Instance;
                connection.ConnectionString = VisualWebPart1._connectionString;
                connection.User = VisualWebPart1._username;
                connection.Password = VisualWebPart1._password;
                #endregion

                //IApplicationFacade application = new ApplicationFacade();
                //var list = new List<Site>();

                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPWeb web = new SPSite(SPContext.Current.Site.ID).OpenWeb(String.Concat(@"/", viewModel.site)))
                    {
                        var anterior = web.AllowUnsafeUpdates;
                        web.AllowUnsafeUpdates = true;

                        try
                        {
                            web.RoleAssignments.Remove(web.SiteGroups[viewModel.nome]);
                        }
                        catch (Exception)
                        {
                            web.RoleAssignments.Remove(web.EnsureUser(viewModel.nome));
                        }

                        web.Update();
                        web.AllowUnsafeUpdates = !anterior;
                    }
                });


                return JsonConvert.SerializeObject(
                   viewModel, new JsonSerializerSettings
                   {
                       ContractResolver = new CamelCasePropertyNamesContractResolver()
                   });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}