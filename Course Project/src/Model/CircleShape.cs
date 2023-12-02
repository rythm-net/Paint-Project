using System;
using System.Drawing;

namespace Draw
{

	[Serializable]
	public class CircleShape : Shape
	{
		#region Constructor

		public CircleShape(RectangleF rect) : base(rect) { }

		public CircleShape(CircleShape rectangle) : base(rectangle) { }

		#endregion

		
		public virtual bool ContainsCircle(PointF point)
		{
			int offSet = 50;

			bool isInsideFigureCalculator = ((point.X - offSet) - Rectangle.X) * ((point.X - offSet) - Rectangle.X) + ((point.Y - offSet) -
				Rectangle.Y) * ((point.Y - offSet) - Rectangle.Y) <= 50 * 50;

			if (isInsideFigureCalculator)
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

