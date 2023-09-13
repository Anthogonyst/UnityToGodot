using System.Collections.Generic;
using Godot;

namespace UnityEngine
{
	public class WWW : CustomYieldInstruction, System.IDisposable
	{
		HTTPRequest requestNode;

		public string url { get { return _url; } }
		string _url = "";

		byte[] _bytes = new byte[0];
		public byte[] bytes { get { return _bytes; } }
		public string text { get { return System.Text.Encoding.Default.GetString(_bytes); } }
		public Texture2D texture { get { return new Texture2D(bytes); } }
		public string error { get; private set; }

		bool _isDone = false;
		public bool isDone { get { return _isDone; } }

		UnityWebRequestMethod _method = UnityWebRequestMethod.Get;


		public override bool keepWaiting
		{
			get { return !_isDone; }
		}


		public Dictionary<string, string> responseHeaders
		{
			get
			{
				Debug.Log("WWW.responseHeaders not implemented");
				return new Dictionary<string, string>();
			}
		}


		public WWW(string url)
		{
			_url = url;
			SendRequest();
		}


		public WWW(string url, WWWForm form)
		{
			_url = url;
			_method = UnityWebRequestMethod.Post;
			SendRequest(System.Text.Encoding.Default.GetString(form.data));
		}


		void SendRequest(string requestData = "")
		{
			if ( _url.StartsWith("file://") )
			{
				string path = _url.Substring(7);
				Debug.Log(_url);

				if ( System.IO.File.Exists(path) )
				{
					_bytes = System.IO.File.ReadAllBytes(path);
				}
				else
				{
					error = "Couldn't find file at path: " + path;
				}

				_isDone = true;
			}
			else
			{
				requestNode = new HTTPRequest()
				{
					UseThreads = true
				};
				UnityEngineAutoLoad.Instance.AddChild(requestNode);

				requestNode.Connect("request_completed", this, "_on_request_completed");

				string[] headers = new string[]
				{
					"Content-Type: application/x-www-form-urlencoded",
					"Content-Length: " + requestData.Length
				};

				requestNode.Request(_url, headers, false, (HTTPClient.Method)_method, requestData);
			}
		}


		void _on_request_completed(int result, int response_code, string[] headers, byte[] body)
		{
			_bytes = body;
			_isDone = true;

			requestNode.Free();
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


		public static string EscapeURL(string s)
		{
            return s.PercentEncode();
		}


		public static string UnEscapeURL(string s)
		{
            return s.Replace('+', ' ').PercentDecode();
		}


		public AudioClip GetAudioClip()
		{
			Debug.Log("Implement getting audio clips");
			return null;
		}


/*		//
		// Static Fields
		//
		private static readonly char[] forbiddenCharacters = new char[] {
			'\0',
			'\u0001',
			'\u0002',
			'\u0003',
			'\u0004',
			'\u0005',
			'\u0006',
			'\a',
			'\b',
			'\t',
			'\n',
			'\v',
			'\f',
			'\r',
			'\u000e',
			'\u000f',
			'\u0010',
			'\u0011',
			'\u0012',
			'\u0013',
			'\u0014',
			'\u0015',
			'\u0016',
			'\u0017',
			'\u0018',
			'\u0019',
			'\u001a',
			'\u001b',
			'\u001c',
			'\u001d',
			'\u001e',
			'\u001f',
			'\u007f'
		};

		private static readonly char[] forbiddenCharactersForNames = new char[] {
			' '
		};

		private static readonly string[] forbiddenHeaderKeys = new string[] {
			"Accept-Charset",
			"Accept-Encoding",
			"Access-Control-Request-Headers",
			"Access-Control-Request-Method",
			"Connection",
			"Content-Length",
			"Cookie",
			"Cookie2",
			"Date",
			"DNT",
			"Expect",
			"Host",
			"Keep-Alive",
			"Origin",
			"Referer",
			"TE",
			"Trailer",
			"Transfer-Encoding",
			"Upgrade",
			"User-Agent",
			"Via",
			"X-Unity-Version"
		};

		//
		// Fields
		//
		private UnityWebRequest _uwr;

		private AssetBundle _assetBundle;

		private Dictionary<string, string> _responseHeaders;

		//
		// Properties
		//
		public AssetBundle assetBundle {
			get {
				throw new System.NotSupportedException("Asset Bundles not supported!");
				return null;
			}
		}

		[Obsolete ("Obsolete msg (UnityUpgradable) -> * UnityEngine.WWWAudioExtensions.GetAudioClip(UnityEngine.WWW)", true)]
		public Object audioClip {
			get {
				return null;
			}
		}

		public byte[] bytes {
			get {
				byte[] result;
				if (!this.WaitUntilDoneIfPossible ()) {
					result = new byte[0];
				} else if (this._uwr.isNetworkError) {
					result = null;
				} else {
					DownloadHandler downloadHandler = this._uwr.downloadHandler;
					if (downloadHandler == null) {
						result = null;
					} else {
						result = downloadHandler.data;
					}
				}
				return result;
			}
		}

		public int bytesDownloaded {
			get {
				return (int)this._uwr.downloadedBytes;
			}
		}

		[Obsolete ("Please use WWW.text instead. (UnityUpgradable) -> text", true)]
		public string data {
			get {
				return this.text;
			}
		}

		public string error {
			get {
				string result;
				if (!this._uwr.isDone) {
					result = null;
				} else if (this._uwr.isNetworkError) {
					result = this._uwr.error;
				} else if (this._uwr.responseCode >= 400L) {
					result = string.Format ("{0} {1}", this._uwr.responseCode, this.GetStatusCodeName (this._uwr.responseCode));
				} else {
					result = null;
				}
				return result;
			}
		}

		public bool isDone {
			get {
				return this._uwr.isDone;
			}
		}

		public override bool keepWaiting {
			get {
				return !this._uwr.isDone;
			}
		}

		[Obsolete ("Obsolete msg (UnityUpgradable) -> * UnityEngine.WWWAudioExtensions.GetMovieTexture(UnityEngine.WWW)", true)]
		public Object movie {
			get {
				return null;
			}
		}

		[EditorBrowsable (EditorBrowsableState.Never), Obsolete ("Obsolete msg (UnityUpgradable) -> * UnityEngine.WWWAudioExtensions.GetAudioClip(UnityEngine.WWW)", true)]
		public Object oggVorbis {
			get {
				return null;
			}
		}

		public float progress {
			get {
				float num = this._uwr.downloadProgress;
				if (num < 0f) {
					num = 0f;
				}
				return num;
			}
		}

		public Dictionary<string, string> responseHeaders {
			get {
				Dictionary<string, string> result;
				if (!this.isDone || this._uwr.isNetworkError) {
					result = null;
				} else {
					if (this._responseHeaders == null) {
						this._responseHeaders = this._uwr.GetResponseHeaders ();
						this._responseHeaders ["STATUS"] = string.Format ("HTTP/1.1 {0} {1}", this._uwr.responseCode, this.GetStatusCodeName (this._uwr.responseCode));
					}
					result = this._responseHeaders;
				}
				return result;
			}
		}

		[Obsolete ("WWW.size is obsolete. Please use WWW.bytesDownloaded instead")]
		public int size {
			get {
				return this.bytesDownloaded;
			}
		}

		public string text {
			get {
				string result;
				if (!this.WaitUntilDoneIfPossible ()) {
					result = "";
				} else if (this._uwr.isNetworkError) {
					result = "";
				} else {
					DownloadHandler downloadHandler = this._uwr.downloadHandler;
					if (downloadHandler == null) {
						result = "";
					} else {
						result = downloadHandler.text;
					}
				}
				return result;
			}
		}

		public Texture2D texture {
			get {
				return this.CreateTextureFromDownloadedData (false);
			}
		}

		public Texture2D textureNonReadable {
			get {
				return this.CreateTextureFromDownloadedData (true);
			}
		}

		public ThreadPriority threadPriority {
			get;
			set;
		}

		public float uploadProgress {
			get {
				float num = this._uwr.uploadProgress;
				if (num < 0f) {
					num = 0f;
				}
				return num;
			}
		}

		public string url {
			get {
				return this._uwr.url;
			}
		}

		//
		// Constructors
		//
		internal WWW (string url, string name, Hash128 hash, uint crc)
		{
			this._uwr = UnityWebRequest.GetAssetBundle (url, new CachedAssetBundle (name, hash), crc);
			this._uwr.Send ();
		}

		public WWW (string url, byte[] postData, Dictionary<string, string> headers)
		{
			string method = (postData != null) ? "POST" : "GET";
			this._uwr = new UnityWebRequest (url, method);
			UploadHandler uploadHandler = new UploadHandlerRaw (postData);
			uploadHandler.contentType = "application/x-www-form-urlencoded";
			this._uwr.uploadHandler = uploadHandler;
			this._uwr.downloadHandler = new DownloadHandlerBuffer ();
			foreach (KeyValuePair<string, string> current in headers) {
				this._uwr.SetRequestHeader (current.Key, current.Value);
			}
			this._uwr.Send ();
		}

		[Obsolete ("This overload is deprecated. Use UnityEngine.WWW.WWW(string, byte[], System.Collections.Generic.Dictionary<string, string>) instead.")]
		public WWW (string url, byte[] postData, Hashtable headers)
		{
			string method = (postData != null) ? "POST" : "GET";
			this._uwr = new UnityWebRequest (url, method);
			UploadHandler uploadHandler = new UploadHandlerRaw (postData);
			uploadHandler.contentType = "application/x-www-form-urlencoded";
			this._uwr.uploadHandler = uploadHandler;
			this._uwr.downloadHandler = new DownloadHandlerBuffer ();
			IEnumerator enumerator = headers.Keys.GetEnumerator ();
			try {
				while (enumerator.MoveNext ()) {
					object current = enumerator.Current;
					this._uwr.SetRequestHeader ((string)current, (string)headers [current]);
				}
			} finally {
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null) {
					disposable.Dispose ();
				}
			}
			this._uwr.Send ();
		}

		public WWW (string url, byte[] postData)
		{
			this._uwr = new UnityWebRequest (url, "POST");
			UploadHandler uploadHandler = new UploadHandlerRaw (postData);
			uploadHandler.contentType = "application/x-www-form-urlencoded";
			this._uwr.uploadHandler = uploadHandler;
			this._uwr.downloadHandler = new DownloadHandlerBuffer ();
			this._uwr.Send ();
		}

		public WWW (string url, WWWForm form)
		{
			this._uwr = UnityWebRequest.Post (url, form);
			this._uwr.Send ();
		}

		public WWW (string url)
		{
			this._uwr = UnityWebRequest.Get (url);
			this._uwr.Send ();
		}

		//
		// Static Methods
		//
		private static void CheckSecurityOnHeaders (string[] headers)
		{
			for (int i = 0; i < headers.Length; i += 2) {
				string[] array = WWW.forbiddenHeaderKeys;
				for (int j = 0; j < array.Length; j++) {
					string b = array [j];
					if (string.Equals (headers [i], b, StringComparison.CurrentCultureIgnoreCase)) {
						throw new ArgumentException ("Cannot overwrite header: " + headers [i]);
					}
				}
				if (headers [i].StartsWith ("Sec-") || headers [i].StartsWith ("Proxy-")) {
					throw new ArgumentException ("Cannot overwrite header: " + headers [i]);
				}
				if (headers [i].IndexOfAny (WWW.forbiddenCharacters) > -1 || headers [i].IndexOfAny (WWW.forbiddenCharactersForNames) > -1 || headers [i + 1].IndexOfAny (WWW.forbiddenCharacters) > -1) {
					throw new ArgumentException ("Cannot include control characters in a HTTP header, either as key or value.");
				}
			}
		}

		public static string EscapeURL (string s, Encoding e)
		{
			string result;
			if (s == null) {
				result = null;
			} else if (s == "") {
				result = "";
			} else if (e == null) {
				result = null;
			} else {
				result = WWWTranscoder.URLEncode (s, e);
			}
			return result;
		}

		public static string EscapeURL (string s)
		{
			return WWW.EscapeURL (s, Encoding.UTF8);
		}

		private static string[] FlattenedHeadersFrom (Dictionary<string, string> headers)
		{
			string[] result;
			if (headers == null) {
				result = null;
			} else {
				string[] array = new string[headers.Count * 2];
				int num = 0;
				foreach (KeyValuePair<string, string> current in headers) {
					array [num++] = current.Key.ToString ();
					array [num++] = current.Value.ToString ();
				}
				result = array;
			}
			return result;
		}

		public static WWW LoadFromCacheOrDownload (string url, CachedAssetBundle cachedBundle, uint crc = 0u)
		{
			return new WWW (url, cachedBundle.name, cachedBundle.hash, crc);
		}

		public static WWW LoadFromCacheOrDownload (string url, Hash128 hash, uint crc)
		{
			return new WWW (url, "", hash, crc);
		}

		public static WWW LoadFromCacheOrDownload (string url, Hash128 hash)
		{
			return WWW.LoadFromCacheOrDownload (url, hash, 0u);
		}

		public static WWW LoadFromCacheOrDownload (string url, int version, uint crc)
		{
			Hash128 hash = new Hash128 (0u, 0u, 0u, (uint)version);
			return WWW.LoadFromCacheOrDownload (url, hash, crc);
		}

		public static WWW LoadFromCacheOrDownload (string url, int version)
		{
			return WWW.LoadFromCacheOrDownload (url, version, 0u);
		}

		internal static Dictionary<string, string> ParseHTTPHeaderString (string input)
		{
			if (input == null) {
				throw new ArgumentException ("input was null to ParseHTTPHeaderString");
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string> (StringComparer.OrdinalIgnoreCase);
			StringReader stringReader = new StringReader (input);
			int num = 0;
			while (true) {
				string text = stringReader.ReadLine ();
				if (text == null) {
					break;
				}
				if (num++ == 0 && text.StartsWith ("HTTP")) {
					dictionary ["STATUS"] = text;
				} else {
					int num2 = text.IndexOf (": ");
					if (num2 != -1) {
						string key = text.Substring (0, num2).ToUpper ();
						string text2 = text.Substring (num2 + 2);
						string str;
						if (dictionary.TryGetValue (key, out str)) {
							text2 = str + "," + text2;
						}
						dictionary [key] = text2;
					}
				}
			}
			return dictionary;
		}

		public static string UnEscapeURL (string s)
		{
			return WWW.UnEscapeURL (s, Encoding.UTF8);
		}

		public static string UnEscapeURL (string s, Encoding e)
		{
			string result;
			if (s == null) {
				result = null;
			} else if (s.IndexOf ('%') == -1 && s.IndexOf ('+') == -1) {
				result = s;
			} else {
				result = WWWTranscoder.URLDecode (s, e);
			}
			return result;
		}

		//
		// Methods
		//
		private Texture2D CreateTextureFromDownloadedData (bool markNonReadable)
		{
			Texture2D result;
			if (!this.WaitUntilDoneIfPossible ()) {
				result = new Texture2D (2, 2);
			} else if (this._uwr.isNetworkError) {
				result = null;
			} else {
				DownloadHandler downloadHandler = this._uwr.downloadHandler;
				if (downloadHandler == null) {
					result = null;
				} else {
					Texture2D texture2D = new Texture2D (2, 2);
					texture2D.LoadImage (downloadHandler.data, markNonReadable);
					result = texture2D;
				}
			}
			return result;
		}

		public void Dispose ()
		{
			this._uwr.Dispose ();
		}

		internal Object GetAudioClipInternal (bool threeD, bool stream, bool compressed, AudioType audioType)
		{
			return WebRequestWWW.InternalCreateAudioClipUsingDH (this._uwr.downloadHandler, this._uwr.url, stream, compressed, audioType);
		}

		internal object GetMovieTextureInternal ()
		{
			return WebRequestWWW.InternalCreateMovieTextureUsingDH (this._uwr.downloadHandler);
		}

		private string GetStatusCodeName (long statusCode)
		{
			string result;
			if (statusCode >= 400L && statusCode <= 416L) {
				switch ((int)(statusCode - 400L)) {
					case 0:
						result = "Bad Request";
						return result;
					case 1:
						result = "Unauthorized";
						return result;
					case 2:
						result = "Payment Required";
						return result;
					case 3:
						result = "Forbidden";
						return result;
					case 4:
						result = "Not Found";
						return result;
					case 5:
						result = "Method Not Allowed";
						return result;
					case 6:
						result = "Not Acceptable";
						return result;
					case 7:
						result = "Proxy Authentication Required";
						return result;
					case 8:
						result = "Request Timeout";
						return result;
					case 9:
						result = "Conflict";
						return result;
					case 10:
						result = "Gone";
						return result;
					case 11:
						result = "Length Required";
						return result;
					case 12:
						result = "Precondition Failed";
						return result;
					case 13:
						result = "Request Entity Too Large";
						return result;
					case 14:
						result = "Request-URI Too Long";
						return result;
					case 15:
						result = "Unsupported Media Type";
						return result;
					case 16:
						result = "Requested Range Not Satisfiable";
						return result;
				}
			}
			if (statusCode >= 200L && statusCode <= 206L) {
				switch ((int)(statusCode - 200L)) {
					case 0:
						result = "OK";
						return result;
					case 1:
						result = "Created";
						return result;
					case 2:
						result = "Accepted";
						return result;
					case 3:
						result = "Non-Authoritative Information";
						return result;
					case 4:
						result = "No Content";
						return result;
					case 5:
						result = "Reset Content";
						return result;
					case 6:
						result = "Partial Content";
						return result;
				}
			}
			if (statusCode >= 300L && statusCode <= 307L) {
				switch ((int)(statusCode - 300L)) {
					case 0:
						result = "Multiple Choices";
						return result;
					case 1:
						result = "Moved Permanently";
						return result;
					case 2:
						result = "Found";
						return result;
					case 3:
						result = "See Other";
						return result;
					case 4:
						result = "Not Modified";
						return result;
					case 5:
						result = "Use Proxy";
						return result;
					case 7:
						result = "Temporary Redirect";
						return result;
				}
			}
			if (statusCode >= 500L && statusCode <= 505L) {
				switch ((int)(statusCode - 500L)) {
					case 0:
						result = "Internal Server Error";
						return result;
					case 1:
						result = "Not Implemented";
						return result;
					case 2:
						result = "Bad Gateway";
						return result;
					case 3:
						result = "Service Unavailable";
						return result;
					case 4:
						result = "Gateway Timeout";
						return result;
					case 5:
						result = "HTTP Version Not Supported";
						return result;
				}
			}
			if (statusCode != 41L) {
				result = "";
			} else {
				result = "Expectation Failed";
			}
			return result;
		}

		public void LoadImageIntoTexture (Texture2D texture)
		{
			if (this.WaitUntilDoneIfPossible ()) {
				if (this._uwr.isNetworkError) {
					Debug.LogError ("Cannot load image: download failed");
				} else {
					DownloadHandler downloadHandler = this._uwr.downloadHandler;
					if (downloadHandler == null) {
						Debug.LogError ("Cannot load image: internal error");
					} else {
						texture.LoadImage (downloadHandler.data, false);
					}
				}
			}
		}

		private bool WaitUntilDoneIfPossible ()
		{
			bool result;
			if (this._uwr.isDone) {
				result = true;
			} else if (this.url.StartsWith ("file://", StringComparison.OrdinalIgnoreCase)) {
				while (!this._uwr.isDone) {
				}
				result = true;
			} else {
				Debug.LogError ("You are trying to load data from a www stream which has not completed the download yet.\nYou need to yield the download or wait until isDone returns true.");
				result = false;
			}
			return result;
		}*/
	}
}