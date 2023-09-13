using Godot;

namespace UnityEngine
{
	class CanvasItemVisibilityHandler : VisibilityHandler
	{
		public override bool IsVisible
		{
			get
			{
				return CanvasItem.Visible;
			}
		}

		CanvasItem CanvasItem;


		public CanvasItemVisibilityHandler(CanvasItem canvasItem)
		{
			this.CanvasItem = canvasItem;
		}
	}
}