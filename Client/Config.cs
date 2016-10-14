using System;
using System.Configuration;

namespace Dwolla
{
  /// <summary>
  ///   Configuration class, wraps around ConfigurationManager
  /// </summary>
  public class Config
  {
    /// <summary>
    ///   The Dwolla API Application ID
    /// </summary>
    public string dwolla_key
    {
      get { return ConfigurationManager.AppSettings["dwolla_key"]; }
      set { ConfigurationManager.AppSettings["dwolla_key"] = value; }
    }

    /// <summary>
    ///   The Dwolla API Application Secret
    /// </summary>
    public string dwolla_secret
    {
      get { return ConfigurationManager.AppSettings["dwolla_secret"]; }
      set { ConfigurationManager.AppSettings["dwolla_secret"] = value; }
    }

    /// <summary>
    ///   Your/OAuth User's Dwolla PIN
    /// </summary>
    public int dwolla_pin
    {
      get { return Convert.ToInt16(ConfigurationManager.AppSettings["dwolla_pin"]); }
      set { ConfigurationManager.AppSettings["dwolla_pin"] = value.ToString(); }
    }

    /// <summary>
    ///   Your/OAuth User's Access Token
    /// </summary>
    public string dwolla_access_token
    {
      get { return ConfigurationManager.AppSettings["dwolla_access_token"]; }
      set { ConfigurationManager.AppSettings["dwolla_access_token"] = value; }
    }

    /// <summary>
    ///   OAuth permissions scope
    /// </summary>
    public string dwolla_oauth_scope
    {
      get { return ConfigurationManager.AppSettings["dwolla_oauth_scope"]; }
      set { ConfigurationManager.AppSettings["dwolla_oauth_scope"] = value; }
    }

    /// <summary>
    ///   Dwolla production host
    /// </summary>
    public string dwolla_production_host
    {
      get { return ConfigurationManager.AppSettings["dwolla_production_host"]; }
      set { ConfigurationManager.AppSettings["dwolla_production_host"] = value; }
    }

    /// <summary>
    ///   Dwolla sandbox host
    /// </summary>
    public string dwolla_sandbox_host
    {
      get { return ConfigurationManager.AppSettings["dwolla_sandbox_host"]; }
      set { ConfigurationManager.AppSettings["dwolla_sandbox_host"] = value; }
    }

    /// <summary>
    ///   Default REST postfix
    /// </summary>
    public string dwolla_default_postfix
    {
      get { return ConfigurationManager.AppSettings["dwolla_default_postfix"]; }
      set { ConfigurationManager.AppSettings["dwolla_default_postfix"] = value; }
    }

    /// <summary>
    ///   Debug flag
    /// </summary>
    public bool dwolla_debug
    {
      get { return Convert.ToBoolean(ConfigurationManager.AppSettings["dwolla_debug"]); }
      set { ConfigurationManager.AppSettings["dwolla_debug"] = value.ToString(); }
    }

    /// <summary>
    ///   Toggle use of Sandbox host (for testing)
    /// </summary>
    public bool dwolla_sandbox
    {
      get { return Convert.ToBoolean(ConfigurationManager.AppSettings["dwolla_sandbox"]); }
      set { ConfigurationManager.AppSettings["dwolla_sandbox"] = value.ToString(); }
    }
  }
}
