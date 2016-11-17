using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint.WebControls;
using System.Configuration;
using SafePointSecurity.Common.Util;
using Microsoft.SharePoint;

namespace SafePointSecurity
{
    [ToolboxItemAttribute(false)]
    public partial class VisualWebPart1 : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public VisualWebPart1()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();

            //Cria e recupera variaveis do propertie bag do site collection:
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPWeb web = new SPSite(SPContext.Current.Site.ID).RootWeb)
                {
                    bool oldState = web.AllowUnsafeUpdates;
                    web.AllowUnsafeUpdates = true;

                    try
                    {
                        if (web.Properties["SafepointSecurity_ConnectionString"] == null)
                            web.Properties.Add("SafepointSecurity_ConnectionString", string.Empty);
                        _connectionString = Cryptography.Decrypter(web.Properties["SafepointSecurity_ConnectionString"]);

                        if (web.Properties["SafepointSecurity_Server"] == null)
                            web.Properties.Add("SafepointSecurity_Server", string.Empty);
                        _server = Cryptography.Decrypter(web.Properties["SafepointSecurity_Server"]);

                        if (web.Properties["SafepointSecurity_Database"] == null)
                            web.Properties.Add("SafepointSecurity_Database", string.Empty);
                        _dataBase = Cryptography.Decrypter(web.Properties["SafepointSecurity_Database"]);

                        if (web.Properties["SafepointSecurity_FarmUser"] == null)
                            web.Properties.Add("SafepointSecurity_FarmUser", string.Empty);
                        _username = web.Properties["SafepointSecurity_FarmUser"];

                        if (web.Properties["SafepointSecurity_FarmPassword"] == null)
                            web.Properties.Add("SafepointSecurity_FarmPassword", string.Empty);
                        _password = Cryptography.Decrypter(web.Properties["SafepointSecurity_FarmPassword"]);

                        web.Properties.Update();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        web.AllowUnsafeUpdates = !oldState;
                    }
                }
            });


        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            var isWebPartInEditMode = this.WebPartManager.DisplayMode == WebPartManager.EditDisplayMode;
            var isPageInEditMode = SPContext.Current.FormContext.FormMode == SPControlMode.Edit;

            _editMode = (isWebPartInEditMode || isPageInEditMode);

            //Atualiza valores das propriedades no propertie bag do sitecollection:
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPWeb web = new SPSite(SPContext.Current.Site.ID).RootWeb)
                {
                    bool oldState = web.AllowUnsafeUpdates;
                    web.AllowUnsafeUpdates = true;

                    try
                    {
                        web.Properties["SafepointSecurity_ConnectionString"] = Cryptography.Encrypter(_connectionString);
                        web.Properties["SafepointSecurity_Server"] = Cryptography.Encrypter(_server);
                        web.Properties["SafepointSecurity_Database"] = Cryptography.Encrypter(_dataBase);
                        web.Properties["SafepointSecurity_FarmUser"] = _username;
                        web.Properties["SafepointSecurity_FarmPassword"] = Cryptography.Encrypter(_password);

                        web.Properties.Update();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        web.AllowUnsafeUpdates = !oldState;
                    }
                }
            });
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }


        #region Properties

        public static string _username;
        [Category("SafePoint - Configurações"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        WebDisplayName("Usuário do Farm")]
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public static string _password;
        [Category("SafePoint - Configurações"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        WebDisplayName("Senha")]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public static string _connectionString;
        [Category("SafePoint - Configurações"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        WebDisplayName("String de Conexão")]
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public static string _server;
        [Category("SafePoint - Configurações"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        WebDisplayName("Servidor(servidor/instância)")]
        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }

        public static string _dataBase;
        [Category("SafePoint - Configurações"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        WebDisplayName("Banco de Dados de Conteúdo")]
        public string DataBase
        {
            get { return _dataBase; }
            set { _dataBase = value; }
        }

        public static bool _editMode;


        #endregion

    }
}
