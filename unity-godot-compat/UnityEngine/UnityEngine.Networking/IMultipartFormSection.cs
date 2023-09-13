namespace UnityEngine.Networking
{
	public interface IMultipartFormSection
	{
		//
		// Properties
		//
		string contentType {
			get;
		}

		string fileName {
			get;
		}

		byte[] sectionData {
			get;
		}

		string sectionName {
			get;
		}
	}
}