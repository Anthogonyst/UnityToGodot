using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Scripting;

namespace UnityEngine.Networking
{
	[StructLayout (LayoutKind.Sequential)]
	public sealed class UnityWebRequest : IDisposable
	{
		//
		// Static Fields
		//
		public const string kHttpVerbDELETE = "DELETE";

		public const string kHttpVerbGET = "GET";

		public const string kHttpVerbHEAD = "HEAD";

		public const string kHttpVerbPOST = "POST";

		public const string kHttpVerbPUT = "PUT";

		public const string kHttpVerbCREATE = "CREATE";

		//
		// Fields
		//
		[NonSerialized]
		internal IntPtr m_Ptr;

		Dictionary<string, string> headers = new Dictionary<string, string>();

		//
		// Properties
		//
		public extern bool chunkedTransfer {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			set;
		}

		public bool disposeDownloadHandlerOnDispose {
			get;
			set;
		}

		public bool disposeUploadHandlerOnDispose {
			get;
			set;
		}

		public extern ulong downloadedBytes {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
		}

		public extern DownloadHandler downloadHandler {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			set;
		}

		public extern float downloadProgress {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
		}

		public extern string error {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool isDone {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
		}

		[Obsolete ("UnityWebRequest.isError has been renamed to isNetworkError for clarity. (UnityUpgradable) -> isNetworkError", false)]
		public bool isError {
			get {
				return this.isNetworkError;
			}
		}

		public extern bool isHttpError {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool isModifiable {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool isNetworkError {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
		}

		private UnityWebRequestMethod _method = UnityWebRequestMethod.Get;
		public string method {
			get {
				string result;
				switch (_method) {
					case UnityWebRequestMethod.Get:
						result = "GET";
						break;
					case UnityWebRequestMethod.Post:
						result = "POST";
						break;
					case UnityWebRequestMethod.Put:
						result = "PUT";
						break;
					case UnityWebRequestMethod.Head:
						result = "HEAD";
						break;
					default:
						result = this.InternalGetCustomMethod ();
						break;
				}
				return result;
			}
			set {
				if (string.IsNullOrEmpty (value)) {
					throw new ArgumentException ("Cannot set a UnityWebRequest's method to an empty or null string");
				}
				string text = value.ToUpper ();
				if (text != null) {
					if (text == "GET") {
						this.InternalSetMethod (UnityWebRequestMethod.Get);
						return;
					}
					if (text == "POST") {
						this.InternalSetMethod (UnityWebRequestMethod.Post);
						return;
					}
					if (text == "PUT") {
						this.InternalSetMethod (UnityWebRequestMethod.Put);
						return;
					}
					if (text == "HEAD") {
						this.InternalSetMethod (UnityWebRequestMethod.Head);
						return;
					}
				}
				this.InternalSetCustomMethod (value.ToUpper ());
			}
		}

		public extern int redirectLimit {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			set;
		}

		public extern long responseCode {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
		}

		public extern int timeout {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			set;
		}

		public extern ulong uploadedBytes {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
		}

		public extern UploadHandler uploadHandler {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			set;
		}

		public extern float uploadProgress {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
		}

		private string _url = "";
		public string url {
			get {
				return _url;
			}
			set {
				if ( !string.IsNullOrEmpty(value) ) {
					_url = value;
				}
			}
		}

		public extern bool useHttpContinue {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			set;
		}

		//
		// Constructors
		//
		~UnityWebRequest ()
		{
			this.DisposeHandlers ();
			this.InternalDestroy ();
		}

		public UnityWebRequest ()
		{
			this.InternalCreate ();
			this.InternalSetDefaults ();
		}

		public UnityWebRequest (string url)
		{
			this.InternalCreate ();
			this.InternalSetDefaults ();
			this.url = url;
		}

		public UnityWebRequest (string url, string method)
		{
			this.InternalCreate ();
			this.InternalSetDefaults ();
			this.url = url;
			this.method = method;
		}

		public UnityWebRequest (string url, string method, DownloadHandler downloadHandler, UploadHandler uploadHandler)
		{
			this.InternalCreate ();
			this.InternalSetDefaults ();
			this.url = url;
			this.method = method;
			/*this.downloadHandler = downloadHandler;
			this.uploadHandler = uploadHandler;*/
		}

		//
		// Static Methods
		//
		public static UnityWebRequest Delete (string uri)
		{
			return new UnityWebRequest (uri, "DELETE");
		}

		public static byte[] GenerateBoundary ()
		{
			byte[] array = new byte[40];
			for (int i = 0; i < 40; i++) {
				int num = Random.Range (48, 110);
				if (num > 57) {
					num += 7;
				}
				if (num > 90) {
					num += 6;
				}
				array [i] = (byte)num;
			}
			return array;
		}

		public static UnityWebRequest Get (string uri)
		{
			return new UnityWebRequest (uri, "GET", new DownloadHandlerBuffer (), null);
		}

		public static UnityWebRequest GetAssetBundle (string uri, Hash128 hash, uint crc)
		{
			return new UnityWebRequest (uri, "GET", new DownloadHandlerAssetBundle (uri, hash, crc), null);
		}

		public static UnityWebRequest GetAssetBundle (string uri)
		{
			return UnityWebRequest.GetAssetBundle (uri, 0u);
		}

		public static UnityWebRequest GetAssetBundle (string uri, uint version, uint crc)
		{
			return new UnityWebRequest (uri, "GET", new DownloadHandlerAssetBundle (uri, version, crc), null);
		}

		public static UnityWebRequest GetAssetBundle (string uri, CachedAssetBundle cachedAssetBundle, uint crc)
		{
			return new UnityWebRequest (uri, "GET", new DownloadHandlerAssetBundle (uri, cachedAssetBundle.name, cachedAssetBundle.hash, crc), null);
		}

		public static UnityWebRequest GetAssetBundle (string uri, uint crc)
		{
			return new UnityWebRequest (uri, "GET", new DownloadHandlerAssetBundle (uri, crc), null);
		}

		[Obsolete ("UnityWebRequest.GetAudioClip is obsolete. Use UnityWebRequestMultimedia.GetAudioClip instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestMultimedia.GetAudioClip(*)", true)]
		public static UnityWebRequest GetAudioClip (string uri, AudioType audioType)
		{
			return null;
		}

		private static string GetErrorDescription (UnityWebRequestError errorCode)
		{
			string result;
			switch (errorCode) {
				case UnityWebRequestError.OK:
					result = "No Error";
					return result;
				case UnityWebRequestError.SDKError:
					result = "Internal Error With Transport Layer";
					return result;
				case UnityWebRequestError.UnsupportedProtocol:
					result = "Specified Transport Protocol is Unsupported";
					return result;
				case UnityWebRequestError.MalformattedUrl:
					result = "URL is Malformatted";
					return result;
				case UnityWebRequestError.CannotResolveProxy:
					result = "Unable to resolve specified proxy server";
					return result;
				case UnityWebRequestError.CannotResolveHost:
					result = "Unable to resolve host specified in URL";
					return result;
				case UnityWebRequestError.CannotConnectToHost:
					result = "Unable to connect to host specified in URL";
					return result;
				case UnityWebRequestError.AccessDenied:
					result = "Remote server denied access to the specified URL";
					return result;
				case UnityWebRequestError.GenericHttpError:
					result = "Unknown/Generic HTTP Error - Check HTTP Error code";
					return result;
				case UnityWebRequestError.WriteError:
					result = "Error when transmitting request to remote server - transmission terminated prematurely";
					return result;
				case UnityWebRequestError.ReadError:
					result = "Error when reading response from remote server - transmission terminated prematurely";
					return result;
				case UnityWebRequestError.OutOfMemory:
					result = "Out of Memory";
					return result;
				case UnityWebRequestError.Timeout:
					result = "Timeout occurred while waiting for response from remote server";
					return result;
				case UnityWebRequestError.HTTPPostError:
					result = "Error while transmitting HTTP POST body data";
					return result;
				case UnityWebRequestError.SSLCannotConnect:
					result = "Unable to connect to SSL server at remote host";
					return result;
				case UnityWebRequestError.Aborted:
					result = "Request was manually aborted by local code";
					return result;
				case UnityWebRequestError.TooManyRedirects:
					result = "Redirect limit exceeded";
					return result;
				case UnityWebRequestError.ReceivedNoData:
					result = "Received an empty response from remote host";
					return result;
				case UnityWebRequestError.SSLNotSupported:
					result = "SSL connections are not supported on the local machine";
					return result;
				case UnityWebRequestError.FailedToSendData:
					result = "Failed to transmit body data";
					return result;
				case UnityWebRequestError.FailedToReceiveData:
					result = "Failed to receive response body data";
					return result;
				case UnityWebRequestError.SSLCertificateError:
					result = "Failure to authenticate SSL certificate of remote host";
					return result;
				case UnityWebRequestError.SSLCipherNotAvailable:
					result = "SSL cipher received from remote host is not supported on the local machine";
					return result;
				case UnityWebRequestError.SSLCACertError:
					result = "Failure to authenticate Certificate Authority of the SSL certificate received from the remote host";
					return result;
				case UnityWebRequestError.UnrecognizedContentEncoding:
					result = "Remote host returned data with an unrecognized/unparseable content encoding";
					return result;
				case UnityWebRequestError.LoginFailed:
					result = "HTTP authentication failed";
					return result;
				case UnityWebRequestError.SSLShutdownFailed:
					result = "Failure while shutting down SSL connection";
					return result;
			}
			result = "Unknown error";
			return result;
		}

		[Obsolete ("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestTexture.GetTexture(*)", true)]
		public static UnityWebRequest GetTexture (string uri, bool nonReadable)
		{
			throw new NotSupportedException ("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead.");
		}

		[Obsolete ("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestTexture.GetTexture(*)", true)]
		public static UnityWebRequest GetTexture (string uri)
		{
			throw new NotSupportedException ("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead.");
		}

		public static UnityWebRequest Head (string uri)
		{
			return new UnityWebRequest (uri, "HEAD");
		}

		public static UnityWebRequest Post (string uri, string postData)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest (uri, "POST");
			byte[] data = null;
			if (!string.IsNullOrEmpty (postData)) {
				string s = WWWTranscoder.URLEncode (postData, Encoding.UTF8);
				data = Encoding.UTF8.GetBytes (s);
			}
			unityWebRequest.uploadHandler = new UploadHandlerRaw (data);
			unityWebRequest.uploadHandler.contentType = "application/x-www-form-urlencoded";
			unityWebRequest.downloadHandler = new DownloadHandlerBuffer ();
			return unityWebRequest;
		}

		public static UnityWebRequest Post (string uri, WWWForm formData)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest (uri, "POST");
			byte[] array = null;
			if (formData != null) {
				array = formData.data;
				if (array.Length == 0) {
					array = null;
				}
			}
			unityWebRequest.uploadHandler = new UploadHandlerRaw (array);
			unityWebRequest.downloadHandler = new DownloadHandlerBuffer ();
			if (formData != null) {
				Dictionary<string, string> headers = formData.headers;
				foreach (KeyValuePair<string, string> current in headers) {
					unityWebRequest.SetRequestHeader (current.Key, current.Value);
				}
			}
			return unityWebRequest;
		}

		public static UnityWebRequest Post (string uri, List<IMultipartFormSection> multipartFormSections)
		{
			byte[] boundary = GenerateBoundary ();
			return Post (uri, multipartFormSections, boundary);
		}

		public static UnityWebRequest Post (string uri, List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest (uri, "POST");
			byte[] data = null;
			if (multipartFormSections != null && multipartFormSections.Count != 0) {
				data = SerializeFormSections (multipartFormSections, boundary);
			}
			unityWebRequest.uploadHandler = new UploadHandlerRaw (data) {
				contentType = "multipart/form-data; boundary=" + Encoding.UTF8.GetString (boundary, 0, boundary.Length)
			};
			unityWebRequest.downloadHandler = new DownloadHandlerBuffer ();
			return unityWebRequest;
		}

		public static UnityWebRequest Post (string uri, Dictionary<string, string> formFields)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest (uri, "POST");
			byte[] data = null;
			if (formFields != null && formFields.Count != 0) {
				data = UnityWebRequest.SerializeSimpleForm (formFields);
			}
			unityWebRequest.uploadHandler = new UploadHandlerRaw (data) {
				contentType = "application/x-www-form-urlencoded"
			};
			unityWebRequest.downloadHandler = new DownloadHandlerBuffer ();
			return unityWebRequest;
		}

		public static UnityWebRequest Put (string uri, string bodyData)
		{
			return new UnityWebRequest (uri, "PUT", new DownloadHandlerBuffer (), new UploadHandlerRaw (Encoding.UTF8.GetBytes (bodyData)));
		}

		public static UnityWebRequest Put (string uri, byte[] bodyData)
		{
			return new UnityWebRequest (uri, "PUT", new DownloadHandlerBuffer (), new UploadHandlerRaw (bodyData));
		}

		public static byte[] SerializeFormSections (List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			byte[] bytes = Encoding.UTF8.GetBytes ("\r\n");
			byte[] bytes2 = WWWForm.DefaultEncoding.GetBytes ("--");
			int num = 0;
			foreach (IMultipartFormSection current in multipartFormSections) {
				num += 64 + current.sectionData.Length;
			}
			List<byte> list = new List<byte> (num);
			foreach (IMultipartFormSection current2 in multipartFormSections) {
				string str = "form-data";
				string sectionName = current2.sectionName;
				string fileName = current2.fileName;
				if (!string.IsNullOrEmpty (fileName)) {
					str = "file";
				}
				string text = "Content-Disposition: " + str;
				if (!string.IsNullOrEmpty (sectionName)) {
					text = text + "; name=\"" + sectionName + "\"";
				}
				if (!string.IsNullOrEmpty (fileName)) {
					text = text + "; filename=\"" + fileName + "\"";
				}
				text += "\r\n";
				string contentType = current2.contentType;
				if (!string.IsNullOrEmpty (contentType)) {
					text = text + "Content-Type: " + contentType + "\r\n";
				}
				list.AddRange (bytes);
				list.AddRange (bytes2);
				list.AddRange (boundary);
				list.AddRange (bytes);
				list.AddRange (Encoding.UTF8.GetBytes (text));
				list.AddRange (bytes);
				list.AddRange (current2.sectionData);
			}
			list.TrimExcess ();
			return list.ToArray ();
		}

		public static byte[] SerializeSimpleForm (Dictionary<string, string> formFields)
		{
			string text = "";
			foreach (KeyValuePair<string, string> current in formFields) {
				if (text.Length > 0) {
					text += "&";
				}
				text = text + Uri.EscapeDataString (current.Key) + "=" + Uri.EscapeDataString (current.Value);
			}
			return Encoding.UTF8.GetBytes (text);
		}

		//
		// Methods
		//
		public void Abort ()
		{
			this.InternalAbort ();
		}

		public void Dispose ()
		{
			if (this.m_Ptr != IntPtr.Zero) {
				this.DisposeHandlers ();
				this.InternalDestroy ();
				GC.SuppressFinalize (this);
			}
		}

		private void DisposeHandlers ()
		{
			if (this.disposeDownloadHandlerOnDispose) {
				DownloadHandler downloadHandler = this.GetDownloadHandler ();
				if (downloadHandler != null) {
					downloadHandler.Dispose ();
				}
			}
			if (this.disposeUploadHandlerOnDispose) {
				UploadHandler uploadHandler = this.GetUploadHandler ();
				if (uploadHandler != null) {
					uploadHandler.Dispose ();
				}
			}
		}

		[GeneratedByOldBindingsGenerator, ThreadAndSerializationSafe]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private extern DownloadHandler GetDownloadHandler ();

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		public extern string GetRequestHeader (string name);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		public extern string GetResponseHeader (string name);

		public Dictionary<string, string> GetResponseHeaders ()
		{
			return headers;
		}

		[GeneratedByOldBindingsGenerator, ThreadAndSerializationSafe]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private extern UploadHandler GetUploadHandler ();

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		internal extern void InternalAbort ();

		internal void InternalCreate ()
		{
			Debug.Log("Implement 'InternalCreate'");
		}

		[GeneratedByOldBindingsGenerator, ThreadAndSerializationSafe]
		[MethodImpl (MethodImplOptions.InternalCall)]
		internal extern void InternalDestroy ();

		internal string InternalGetCustomMethod ()
		{
			Debug.Log("InternalGetCustomMethod");
			return "";
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		internal extern int InternalGetError ();

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		internal extern int InternalGetMethod ();

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private extern string InternalGetUrl ();

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		internal extern void InternalSetCustomMethod (string customMethodName);

		private void InternalSetDefaults ()
		{
			this.disposeDownloadHandlerOnDispose = true;
			this.disposeUploadHandlerOnDispose = true;
		}

		internal void InternalSetMethod (UnityWebRequestMethod methodType)
		{
			_method = methodType;
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		internal extern void InternalSetRequestHeader (string name, string value);

		public AsyncOperation Send ()
		{
			AsyncOperation asyncOp = new AsyncOperation();

			ThreadPool.QueueUserWorkItem(HandleRequest, asyncOp);

			return asyncOp;
		}

		void HandleRequest(object state)
		{
			AsyncOperation asyncOp = (AsyncOperation)state;
			Godot.HTTPClient client = new Godot.HTTPClient();

			List<string> headersList = new List<string>();

			headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko");
			headers.Add("Content-Type", "text/html; charset=utf-8");
			headers.Add("Accept", "*/*");

			foreach (string key in headers.Keys)
			{
				headersList.Add(key + ": " + headers[key]);
			}

			client.Request((Godot.HTTPClient.Method)_method, url, headersList.ToArray());

			do
			{
				client.Poll();
				Thread.Sleep(100);
				Debug.Log(client.GetStatus());
				Debug.Log(client.GetResponseBodyLength());
			}
			while (client.GetStatus() == Godot.HTTPClient.Status.Connected || client.GetStatus() == Godot.HTTPClient.Status.Requesting);

			Debug.Log("Stat: " + client.GetStatus());
			Debug.Log(client.GetResponseBodyLength());
			Debug.Log(Encoding.Default.GetString(client.ReadResponseBodyChunk()));
			Debug.Log("Req: " + client.GetStatus());

			asyncOp.isDone = true;
		}

		public void SetRequestHeader (string name, string value)
		{
			if (string.IsNullOrEmpty (name)) {
				throw new ArgumentException ("Cannot set a Request Header with a null or empty name");
			}
			if (value == null) {
				throw new ArgumentException ("Cannot set a Request header with a null");
			}

			this.InternalSetRequestHeader (name, value);
		}

		//
		// Nested Types
		//
		internal enum UnityWebRequestError
		{
			OK,
			Unknown,
			SDKError,
			UnsupportedProtocol,
			MalformattedUrl,
			CannotResolveProxy,
			CannotResolveHost,
			CannotConnectToHost,
			AccessDenied,
			GenericHttpError,
			WriteError,
			ReadError,
			OutOfMemory,
			Timeout,
			HTTPPostError,
			SSLCannotConnect,
			Aborted,
			TooManyRedirects,
			ReceivedNoData,
			SSLNotSupported,
			FailedToSendData,
			FailedToReceiveData,
			SSLCertificateError,
			SSLCipherNotAvailable,
			SSLCACertError,
			UnrecognizedContentEncoding,
			LoginFailed,
			SSLShutdownFailed,
			NoInternetConnection
		}

		internal enum UnityWebRequestMethod
		{
			Get,
			Head,
			Post,
			Put,
			Delete,
			Options,
			Trace,
			Connect,
			Max
		}
	}
}
