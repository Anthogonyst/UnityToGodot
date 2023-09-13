namespace UnityEngine
{
	public static class Resources
	{
		public static AsyncOperation UnloadUnusedAssets()
		{
			AsyncOperation asyncOp = new AsyncOperation();

			Debug.Log("Resources.UnloadUnusedAssets not implemented");
			asyncOp.isDone = true;

			return asyncOp;
		}
	}
}