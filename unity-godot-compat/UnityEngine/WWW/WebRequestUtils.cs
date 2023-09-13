using System;
using System.Text.RegularExpressions;
using UnityEngine.Scripting;

namespace UnityEngineInternal
{
	internal static class WebRequestUtils
	{
		//
		// Static Fields
		//
		private static Regex domainRegex = new Regex ("^\\s*\\w+(?:\\.\\w+)+\\s*$");

		//
		// Static Methods
		//
		internal static string MakeInitialUrl (string targetUrl, string localUrl)
		{
			string result;
			if (targetUrl.StartsWith ("jar:file://")) {
				result = targetUrl;
			} else {
				Uri uri = new Uri (localUrl);
				if (targetUrl.StartsWith ("//")) {
					targetUrl = uri.Scheme + ":" + targetUrl;
				}
				if (targetUrl.StartsWith ("/")) {
					targetUrl = uri.Scheme + "://" + uri.Host + targetUrl;
				}
				if (WebRequestUtils.domainRegex.IsMatch (targetUrl)) {
					targetUrl = uri.Scheme + "://" + targetUrl;
				}
				Uri uri2 = null;
				try {
					uri2 = new Uri (targetUrl);
				} catch (FormatException ex) {
					try {
						uri2 = new Uri (uri, targetUrl);
					} catch (FormatException) {
						throw ex;
					}
				}
				result = ((!targetUrl.Contains ("%")) ? uri2.AbsoluteUri : uri2.OriginalString);
			}
			return result;
		}

		[RequiredByNativeCode]
		internal static string RedirectTo (string baseUri, string redirectUri)
		{
			Uri uri;
			if (redirectUri [0] == '/') {
				uri = new Uri (redirectUri, UriKind.Relative);
			} else {
				uri = new Uri (redirectUri, UriKind.RelativeOrAbsolute);
			}
			string absoluteUri;
			if (uri.IsAbsoluteUri) {
				absoluteUri = uri.AbsoluteUri;
			} else {
				Uri baseUri2 = new Uri (baseUri, UriKind.Absolute);
				Uri uri2 = new Uri (baseUri2, uri);
				absoluteUri = uri2.AbsoluteUri;
			}
			return absoluteUri;
		}
	}
}
