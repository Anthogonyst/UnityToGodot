using UnityEngine.Scripting;

namespace UnityEngine
{
	[UsedByNativeCode]
	public struct CachedAssetBundle
	{
		//
		// Fields
		//
		private string m_Name;

		private Hash128 m_Hash;

		//
		// Properties
		//
		public Hash128 hash {
			get {
				return this.m_Hash;
			}
			set {
				this.m_Hash = value;
			}
		}

		public string name {
			get {
				return this.m_Name;
			}
			set {
				this.m_Name = value;
			}
		}

		//
		// Constructors
		//
		public CachedAssetBundle (string name, Hash128 hash)
		{
			this.m_Name = name;
			this.m_Hash = hash;
		}
	}
}