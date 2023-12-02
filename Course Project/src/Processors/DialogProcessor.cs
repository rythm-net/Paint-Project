using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Draw
{
	

	[Serializable]
	public class DialogProcessor : DisplayProcessor
	{
		#region Constructor

		public DialogProcessor(){}

		#endregion

		#region Properties

		private readonly Random rnd = new Random();
		private FileStream fileStream;
		private readonly BinaryFormatter formatter = new BinaryFormatter();

		private readonly string path = @"D:\temp\figures.bin";


		private readonly Color defaultFillColor = Color.White;

		private readonly Color defaultStrokeColor = Color.Black;

	
		private Shape selection;
		public Shape Selection
		{
			get { return selection; }
			set { selection = value; }
		}

		
		private bool isDragging;
		public bool IsDragging
		{
			get { return isDragging; }
			set { isDragging = value; }
		}

		
		private PointF lastLocation;
		public PointF LastLocation
		{
			get { return lastLocation; }
			set { lastLocation = value; }
		}
		#endregion

		public void Save()
		{

			if (File.Exists(this.path))
			{
				File.Delete(this.path);
			}

			this.fileStream = File.Create(this.path);

			this.formatter.Serialize(fileStream, ShapeList);

			this.fileStream.Close();
		}

		public void Load()
		{
			object obj = null;

			if (File.Exists(this.path))
			{
				this.fileStream = File.OpenRead(this.path);

				obj = this.formatter.Deserialize(this.fileStream);

				var res = (List<Shape>)obj;

				res.ForEach(s => ShapeList.Add(s));

				this.fileStream.Close();
			}
		}

		public void AddRandomRectangle()
		{
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			RectangleShape rect = new RectangleShape(new Rectangle(x, y, 100, 200))
			{
				FillColor = defaultFillColor,

				StrokeColor = defaultStrokeColor
			};

			ShapeList.Add(rect);
		}

		public void AddEllipse()
		{

			EllipseShape ellipse = new EllipseShape(new Rectangle(105, 155, 350, 150))
			{
				FillColor = defaultFillColor,


				StrokeColor = defaultStrokeColor
			};

			ShapeList.Add(ellipse);
		}

	
		public void AddCircle()
		{

			CircleShape circle = new CircleShape(new Rectangle(680, 300, 100, 100))
			{
				FillColor = defaultFillColor,

				StrokeColor = defaultStrokeColor
			};

			ShapeList.Add(circle);
		}

		
		public void CopyAndAddRectangle(int widthOfCopiedFigure, int heightOfCopiedFigure,
			Color fillColorOfCopiedFigure, Color strokeColorOfCopiedFigure, string name, string group)
		{
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			RectangleShape rect = new RectangleShape(new Rectangle(x, y, widthOfCopiedFigure, heightOfCopiedFigure))
			{
				FillColor = fillColorOfCopiedFigure,

				Name = name,

				Group = group,

				StrokeColor = strokeColorOfCopiedFigure
			};

			ShapeList.Add(rect);
		}

		public void CopyAndAddCircle(int widthOfCopiedFigure, int heightOfCopiedFigure,
			Color fillColorOfCopiedFigure, Color strokeColorOfCopiedFigure, string name, string group)
		{
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			EllipseShape ellipse = new EllipseShape(new Rectangle(x, y, widthOfCopiedFigure, heightOfCopiedFigure))
			{
				FillColor = fillColorOfCopiedFigure,

				Name = name,

				Group = group,

				StrokeColor = strokeColorOfCopiedFigure
			};

			ShapeList.Add(ellipse);
		}

		public Shape ContainsPoint(PointF point)
		{
			for (int counter = ShapeList.Count - 1; counter >= 0; counter--)
			{

				if (ShapeList[counter].Contains(point))
				{
					return ShapeList[counter];
				}
			}
			return null;
		}

		public void TranslateTo(PointF points)
		{
			if (selection != null)
			{

				selection.Location = new PointF(selection.Location.X + points.X - lastLocation.X, selection.Location.Y + points.Y - lastLocation.Y);

				lastLocation = points;
			}
		}
	}
}
