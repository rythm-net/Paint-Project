using System;
using System.Drawing;

namespace Draw
{

	[Serializable]
	public class EllipseShape : Shape
	{
		#region Constructor

		public EllipseShape(RectangleF rect) : base(rect) { }

		public EllipseShape(CircleShape rectangle) : base(rectangle) { }

		#endregion

	
		public virtual bool ContainsEllipse(PointF point)
		{

			Boolean isInsideFigureCalculate = ((point.X - 75) - Rectangle.X) * ((point.X - 60) - Rectangle.X) + ((point.Y - 100) -
				Rectangle.Y) * ((point.Y - 100) - Rectangle.Y) <= 40 * 170;

			if (isInsideFigureCalculate)
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

			grfx.FillEllipse(new SolidBrush(FillColor), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
			grfx.DrawEllipse(new Pen(StrokeColor), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);

		}
	}
}


