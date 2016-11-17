using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SafePointSecurity.Infrastructure.Interfaces;
using System.Data;
using System.Data.SqlClient;
using SafePointSecurity.Common.Exceptions;
using SafePointSecurity.Model;

namespace SafePointSecurity.Infrastructure.Repository.SqlServer
{
    public class PermissaoRepository : IPermissaoRepository
    {
        private enum ResultLine
        {
            SiteId = 0,
            GrupoNome = 1,
            PermissaoNivel = 2,
            SiteUrl = 3,
            SiteNome = 4,
            UsuarioId = 5,
            UsuarioLogin = 6,
            UsuarioNome = 7,
            GrupoID = 8,
            WebId = 9,
        }

        private const string query = @"
SELECT  
    T5.SiteId AS [siteID],    
    T5.GroupName AS [grupoNome],
    [permissaoNivel] = 
        CASE
            WHEN T5.RoleTitle = '$Resources:fpext,0x001C003Du' THEN 'Colaboração'
            WHEN T5.RoleTitle = '$Resources:fpext,0x001C003Fu' THEN 'Leitura'
            WHEN T5.RoleTitle = '$Resources:fpext,0x001C003Cu' THEN 'Designer'
            WHEN T5.RoleTitle = '$Resources:fpext,0x001C003Bu' THEN 'Controle Total'
            WHEN T5.RoleTitle = '$Resources:fpext,0x001C003Eu' THEN 'Edição'
            ELSE T5.RoleTitle
        END,
    T5.FullUrl AS [siteURL],
    T5.WebName AS [siteNome],
    T6.tp_ID AS [usuarioID],
    T6.tp_Login AS [usuarioLogin],
    T6.tp_Title AS [usuarioNome],
    T5.groupid AS [grupoID],
    T5.WebId AS [webID]
FROM(
	SELECT 
		T3.SiteId, T3.GroupId, T3.Title [GroupName], T3.MemberID,
		T3.RoleTitle, T3.PrincipalId, T3.ScopeId, T3.RoleID,
		T4.FullUrl, T4.WebName, T4.WebId
	FROM(
		SELECT 
			T1.SiteId, T1.GroupId, T1.Title, r.Title [RoleTitle],
			T1.MemberId, T2.PrincipalId, T2.ScopeId, T2.Roleid
		FROM(
			SELECT 
				g.SiteId, g.ID, g.Title, gm.GroupId, gm.MemberId
			FROM Groups g INNER JOIN GroupMembership gm ON g.SiteId = gm.SiteId AND gm.GroupId = g.ID) T1
		INNER JOIN RoleAssignment T2 ON T1.ID = T2.PrincipalId AND T1.SiteId = T2.SiteId
		INNER JOIN Roles r ON r.SiteId = T1.SiteID AND r.RoleID = T2.RoleID) T3 
	INNER JOIN(
		SELECT 
			w.SiteID, w.FullUrl, w.Title [WebName], 
			w.ScopeID, p.webid, p.DelTransId 
		FROM Webs w LEFT OUTER JOIN Perms p ON p.ScopeUrl = w.FullUrl WHERE p.DelTransId <> '0x') T4 
	ON T3.ScopeId = T4.ScopeId AND T3.SiteId = T4.SiteId)  T5 
INNER JOIN UserInfo T6 ON T5.SiteId = T6.tp_SiteId AND T5.memberId = T6.tp_id
UNION ALL 
SELECT 
	K.SiteId AS [siteID], NULL AS [GroupName],
    [permissaoNivel] = 
            CASE
                WHEN K.RoleTitle = '$Resources:fpext,0x001C003Du' THEN 'Colaboração'
                WHEN K.RoleTitle = '$Resources:fpext,0x001C003Fu' THEN 'Leitura'
                WHEN K.RoleTitle = '$Resources:fpext,0x001C003Cu' THEN 'Designer'
                WHEN K.RoleTitle = '$Resources:fpext,0x001C003Bu' THEN 'Controle Total'
                WHEN K.RoleTitle = '$Resources:fpext,0x001C003Eu' THEN 'Edição'
                ELSE K.RoleTitle
            END,
    K.FullUrl AS [siteURL],	K.WebName AS [siteNome], ui.tp_ID AS [usuarioID], 
    ui.tp_Login AS [usuarioLogin], ui.tp_Title AS [usuarioNome], '' AS [grupoId], K.WebId AS [webID]
FROM(
	SELECT 
		X.SiteId, X.FullUrl, X.Title [WebName], 
		ra.PrincipalId, r.Title [RoleTitle], X.WebId 
	FROM(
		SELECT 
			w.SiteID, w.FullUrl, w.Title, 
			w.ScopeID, p.webid, p.DelTransId
		FROM Webs w LEFT OUTER JOIN Perms p
		ON p.ScopeUrl = w.FullUrl WHERE P.DelTransId <> '0x') X
	INNER JOIN RoleAssignment ra ON X.ScopeId = ra.ScopeId
	INNER JOIN Roles r ON r.SiteId = X.SiteId AND ra.RoleId = r.RoleId) K
INNER JOIN UserInfo ui ON ui.tp_SiteID = K.SiteId AND ui.tp_ID = K.PrincipalId";

        public bool Save(Model.Permissao entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Model.Permissao entity)
        {
            throw new NotImplementedException();
        }

        public List<Model.Permissao> FindAll(string connectionString = null)
        {
            if (String.IsNullOrEmpty(connectionString))
                throw new SafePointStringConnectionException();

            SqlConnection conexao = null;

            try
            {
                conexao = new SqlConnection(connectionString);
                var command = new SqlCommand(query, conexao);
                conexao.Open();

                var reader = command.ExecuteReader();
                var list = new List<Permissao>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var permissao = new Permissao();

                        permissao.Site = new Site
                        {
                            Nome = reader.GetString((int)ResultLine.SiteNome),
                            Url = reader.GetString((int)ResultLine.SiteUrl),
                            Id = (Guid)reader.GetValue((int)ResultLine.WebId),
                        };

                        permissao.NivelPermissao = new NivelPermissao
                        {
                            Nome = reader.GetString((int)ResultLine.PermissaoNivel)
                        };

                        if (!reader.IsDBNull((int)ResultLine.GrupoNome))
                            permissao.Grupo = new Grupo
                            {
                                Nome = reader.GetString((int)ResultLine.GrupoNome),
                                Id = reader.GetInt32((int)ResultLine.GrupoID),
                            };

                        permissao.Usuario = new Usuario
                        {
                            Nome = reader.GetString((int)ResultLine.UsuarioNome),
                            Id = reader.GetInt32((int)ResultLine.UsuarioId),
                            Login = reader.GetString((int)ResultLine.UsuarioLogin)
                        };

                        list.Add(permissao);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conexao.State == ConnectionState.Open)
                    conexao.Close();
            }
        }
    }
}
