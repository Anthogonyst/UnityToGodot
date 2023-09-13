using Godot;

namespace UnityEngine
{
	class SpatialVisibilityHandler : VisibilityHandler
	{
		public override bool IsVisible
		{
			get
			{
				return spatial.Visible;
			}
		}

		Spatial spatial;


		public SpatialVisibilityHandler(Spatial spatial)
		{
			this.spatial = spatial;
		}
	}
}