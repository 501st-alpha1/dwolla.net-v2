using System;
using System.Collections.Generic;
using System.Web;
using Dwolla.SerializableTypes;

namespace Dwolla
{
  public class OAuth : Rest
  {
    /// <summary>
    ///   Returns an OAuth permissions page URL. If no redirect is set,
    ///   the redirect in the Dwolla Application Settings will be use
    ///   If no scope is set, the scope in the settings object will be use
    /// </summary>
    /// <param name="redirect">Redirect destination</param>
    /// <param name="scope">
    ///   OAuth scope string to override the scope
    ///   within the application settings
    /// </param>
    /// <returns>OAuth permissions page Uri</returns>
    public Uri GenAuthUrl(string redirect = null, string scope = null, bool verified_account = false)
    {
      var b = new UriBuilder(
        (C.dwolla_sandbox ? C.dwolla_sandbox_host : C.dwolla_production_host)
        + "oauth/v2/authenticate?client_id=" + HttpUtility.UrlEncode(C.dwolla_key)
        + "&response_type=code&scope=" + (scope ?? C.dwolla_oauth_scope)
        + (redirect == null ? "" : "&redirect_uri=" + HttpUtility.UrlEncode(redirect))
        + (verified_account ? "&verified_account=true" : ""));
      return b.Uri;
    }

    /// <summary>
    ///   Returns an OAuth token + refresh pair in an array. If no redirect
    ///   is set, the redirect in the Dwolla Application Settings will be used
    /// </summary>
    /// <param name="code">Code from redirect response</param>
    /// <param name="redirect">Redirect destination</param>
    /// <returns>OAuthResponse object</returns>
    public OAuthResponse Get(string code, string redirect = null)
    {
      var data = new Dictionary<string, string>
      {
        {"client_id", C.dwolla_key},
        {"client_secret", C.dwolla_secret},
        {"grant_type", "authorization_code"},
        {"code", code}
      };
      if (redirect != null) data["redirect_uri"] = redirect;

      var response = Post("/token", data, "/oauth/v2");

      var oar = Jss.Deserialize<OAuthResponse>(response);
      if (oar.access_token != null) return oar;
      throw new ApiException(Jss.Deserialize<OAuthError>(response).error_description);
    }

    /// <summary>
    ///   Returns a newly refreshed access token and refresh token pair.
    /// </summary>
    /// <param name="refreshToken">Refresh token from initial OAuth handshake.</param>
    /// <returns>OAuthResponse object</returns>
    public OAuthResponse Refresh(string refreshToken)
    {
      var response = Post("/token", new Dictionary<string, string>
      {
        {"client_id", C.dwolla_key},
        {"client_secret", C.dwolla_secret},
        {"grant_type", "refresh_token"},
        {"refresh_token", refreshToken}
      }, "/oauth/v2");

      var oar = Jss.Deserialize<OAuthResponse>(response);
      if (oar.access_token != null) return oar;
      throw new OAuthException(Jss.Deserialize<OAuthError>(response).error_description);
    }

    /// <summary>
    ///   Returns a "catalog" of endpoints that are available for use with the current/passed OAuth token.
    /// </summary>
    /// <param name="altToken">Alternate OAuth token</param>
    /// <returns>OAuthCatalog object</returns>
    public OAuthCatalog Catalog(string altToken = null)
    {
      var response = Get("/catalog", new Dictionary<string, string> { { "oauth_token", altToken ?? C.dwolla_access_token } });

      var cat = Jss.Deserialize<OAuthCatalog>(response);
      if (cat.Success) return cat;
      throw new OAuthException(cat.Message);
    }
  }
}
