using System;
using System.Drawing;

namespace Draw
{

	[Serializable]
	public class RectangleShapeWithLine : Shape
	{
		#region Constructor

		public RectangleShapeWithLine(RectangleF rect) : base(rect) { }

		public RectangleShapeWithLine(RectangleShape rectangle) : base(rectangle) { }

		#endregion

		public override bool Contains(PointF point)
		{
			if (base.Contains(point))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

	
		public override void DrawSelf(Graphics grfx)
		{
			base.DrawSelf(grfx);

			grfx.FillRectangle(new SolidBrush(FillColor), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
			grfx.DrawRectangle(new Pen(StrokeColor), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);

			grfx.DrawLine(Pens.Black, Rectangle.Right, Rectangle.Top, Rectangle.Left, Rectangle.Bottom);
		}
	}
}
