using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine.Internal;

namespace UnityEngine
{
	public class WWWForm
	{
		//
		// Fields
		//
		private List<byte[]> formData;

		private List<string> fieldNames;

		private List<string> fileNames;

		private List<string> types;

		private byte[] boundary;

		private bool containsFiles = false;

		//
		// Static Properties
		//
		internal static Encoding DefaultEncoding {
			get {
				return Encoding.ASCII;
			}
		}

		//
		// Properties
		//
		public byte[] data {
			get {
				byte[] result;
				if (this.containsFiles) {
					byte[] bytes = DefaultEncoding.GetBytes ("--");
					byte[] bytes2 = DefaultEncoding.GetBytes ("\r\n");
					byte[] bytes3 = DefaultEncoding.GetBytes ("Content-Type: ");
					byte[] bytes4 = DefaultEncoding.GetBytes ("Content-disposition: form-data; name=\"");
					byte[] bytes5 = DefaultEncoding.GetBytes ("\"");
					byte[] bytes6 = DefaultEncoding.GetBytes ("; filename=\"");
					using (MemoryStream memoryStream = new MemoryStream (1024)) {
						for (int i = 0; i < this.formData.Count; i++) {
							memoryStream.Write (bytes2, 0, bytes2.Length);
							memoryStream.Write (bytes, 0, bytes.Length);
							memoryStream.Write (this.boundary, 0, this.boundary.Length);
							memoryStream.Write (bytes2, 0, bytes2.Length);
							memoryStream.Write (bytes3, 0, bytes3.Length);
							byte[] bytes7 = Encoding.UTF8.GetBytes (this.types [i]);
							memoryStream.Write (bytes7, 0, bytes7.Length);
							memoryStream.Write (bytes2, 0, bytes2.Length);
							memoryStream.Write (bytes4, 0, bytes4.Length);
							string text = "";
							string text2 = this.fieldNames [i];
							if (!WWWTranscoder.SevenBitClean (text2, Encoding.UTF8) || text2.IndexOf ("=?") > -1) {
								text2 = string.Concat (new string[] {
									"=?",
									text,
									"?Q?",
									WWWTranscoder.QPEncode (text2, Encoding.UTF8),
									"?="
								});
							}
							byte[] bytes8 = Encoding.UTF8.GetBytes (text2);
							memoryStream.Write (bytes8, 0, bytes8.Length);
							memoryStream.Write (bytes5, 0, bytes5.Length);
							if (this.fileNames [i] != null) {
								string text3 = this.fileNames [i];
								if (!WWWTranscoder.SevenBitClean (text3, Encoding.UTF8) || text3.IndexOf ("=?") > -1) {
									text3 = string.Concat (new string[] {
										"=?",
										text,
										"?Q?",
										WWWTranscoder.QPEncode (text3, Encoding.UTF8),
										"?="
									});
								}
								byte[] bytes9 = Encoding.UTF8.GetBytes (text3);
								memoryStream.Write (bytes6, 0, bytes6.Length);
								memoryStream.Write (bytes9, 0, bytes9.Length);
								memoryStream.Write (bytes5, 0, bytes5.Length);
							}
							memoryStream.Write (bytes2, 0, bytes2.Length);
							memoryStream.Write (bytes2, 0, bytes2.Length);
							byte[] array = this.formData [i];
							memoryStream.Write (array, 0, array.Length);
						}
						memoryStream.Write (bytes2, 0, bytes2.Length);
						memoryStream.Write (bytes, 0, bytes.Length);
						memoryStream.Write (this.boundary, 0, this.boundary.Length);
						memoryStream.Write (bytes, 0, bytes.Length);
						memoryStream.Write (bytes2, 0, bytes2.Length);
						result = memoryStream.ToArray ();
						return result;
					}
				}
				byte[] bytes10 = DefaultEncoding.GetBytes ("&");
				byte[] bytes11 = DefaultEncoding.GetBytes ("=");
				using (MemoryStream memoryStream2 = new MemoryStream (1024)) {
					for (int j = 0; j < this.formData.Count; j++) {
						byte[] array2 = WWWTranscoder.URLEncode (Encoding.UTF8.GetBytes (this.fieldNames [j]));
						byte[] toEncode = this.formData [j];
						byte[] array3 = WWWTranscoder.URLEncode (toEncode);
						if (j > 0) {
							memoryStream2.Write (bytes10, 0, bytes10.Length);
						}
						memoryStream2.Write (array2, 0, array2.Length);
						memoryStream2.Write (bytes11, 0, bytes11.Length);
						memoryStream2.Write (array3, 0, array3.Length);
					}
					result = memoryStream2.ToArray ();
				}
				return result;
			}
		}

		public Dictionary<string, string> headers {
			get {
				Dictionary<string, string> dictionary = new Dictionary<string, string> ();
				if (this.containsFiles) {
					dictionary ["Content-Type"] = "multipart/form-data; boundary=\"" + Encoding.UTF8.GetString (this.boundary, 0, this.boundary.Length) + "\"";
				} else {
					dictionary ["Content-Type"] = "application/x-www-form-urlencoded";
				}
				return dictionary;
			}
		}

		//
		// Constructors
		//
		public WWWForm ()
		{
			this.formData = new List<byte[]> ();
			this.fieldNames = new List<string> ();
			this.fileNames = new List<string> ();
			this.types = new List<string> ();
			this.boundary = new byte[40];
			for (int i = 0; i < 40; i++) {
				int num = Random.Range (48, 110);
				if (num > 57) {
					num += 7;
				}
				if (num > 90) {
					num += 6;
				}
				boundary [i] = (byte)num;
			}
		}

		//
		// Methods
		//
		[ExcludeFromDocs]
		public void AddBinaryData (string fieldName, byte[] contents)
		{
			this.AddBinaryData (fieldName, contents, null, null);
		}

		[ExcludeFromDocs]
		public void AddBinaryData (string fieldName, byte[] contents, string fileName)
		{
			this.AddBinaryData (fieldName, contents, fileName, null);
		}

		public void AddBinaryData (string fieldName, byte[] contents, [DefaultValue ("null")] string fileName, [DefaultValue ("null")] string mimeType)
		{
			this.containsFiles = true;
			bool flag = contents.Length > 8 && contents [0] == 137 && contents [1] == 80 && contents [2] == 78 && contents [3] == 71 && contents [4] == 13 && contents [5] == 10 && contents [6] == 26 && contents [7] == 10;
			if (fileName == null) {
				fileName = fieldName + ((!flag) ? ".dat" : ".png");
			}
			if (mimeType == null) {
				if (flag) {
					mimeType = "image/png";
				} else {
					mimeType = "application/octet-stream";
				}
			}
			this.fieldNames.Add (fieldName);
			this.fileNames.Add (fileName);
			this.formData.Add (contents);
			this.types.Add (mimeType);
		}

		public void AddField (string fieldName, string value)
		{
			this.AddField (fieldName, value, Encoding.UTF8);
		}

		public void AddField (string fieldName, string value, Encoding e)
		{
			this.fieldNames.Add (fieldName);
			this.fileNames.Add (null);
			this.formData.Add (e.GetBytes (value));
			this.types.Add ("text/plain; charset=\"" + e.WebName + "\"");
		}

		public void AddField (string fieldName, int i)
		{
			this.AddField (fieldName, i.ToString ());
		}
	}
}
