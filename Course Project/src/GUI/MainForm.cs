using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Draw
{
	
	public partial class MainForm : Form
	{
		
		private DialogProcessor dialogProcessor = new DialogProcessor();

		private readonly Random rnd = new Random();

		public MainForm()
		{
		
			InitializeComponent();
		}

		
		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Close();
		}
		
		
		void ViewPortPaint(object sender, PaintEventArgs e)
		{
			dialogProcessor.ReDraw(sender, e);
		}
		
		void DrawRectangleSpeedButtonClick(object sender, EventArgs e)
		{
			dialogProcessor.AddRandomRectangle();
			
			statusBar.Items[0].Text = "Последно действие: Рисуване на правоъгълник";
			
			viewPort.Invalidate();
		}

		void ViewPortMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (pickUpSpeedButton.Checked) {
				dialogProcessor.Selection = dialogProcessor.ContainsPoint(e.Location);
				if (dialogProcessor.Selection != null) {
					statusBar.Items[0].Text = "Последно действие: Селекция на примитив";
					dialogProcessor.IsDragging = true;
					dialogProcessor.LastLocation = e.Location;
					viewPort.Invalidate();
				}
			}
			viewPort.Invalidate();
		}

		
		void ViewPortMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (dialogProcessor.IsDragging) {
				if (dialogProcessor.Selection != null) statusBar.Items[0].Text = "Последно действие: Влачене";
				dialogProcessor.TranslateTo(e.Location);
				viewPort.Invalidate();
			}
		}

	
		void ViewPortMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			dialogProcessor.IsDragging = false;
		}

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
			ChangeFillColorRandomly();
			ChangeStrokeColorRandomly();
		}
		private void ChangeFillColorRandomly()
		{
			Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));


			dialogProcessor.Selection.FillColor = randomColor;

			viewPort.Invalidate();
		}
		private void ChangeStrokeColorRandomly()
		{
			Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

			dialogProcessor.Selection.StrokeColor = randomColor;

			base.Invalidate();
		}

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
			ChangeFillColorOfFigure();
		}
		private void ChangeFillColorOfFigure()
		{
			if (colorDialog1.ShowDialog() == DialogResult.OK)
			{
				dialogProcessor.Selection.FillColor = colorDialog1.Color;
			}
			viewPort.Invalidate();
		}

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
			DrawCircleOnClick();
		}
		private void DrawCircleOnClick()
		{
			dialogProcessor.AddCircle();
			viewPort.Invalidate();
		}

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
			DrawEllipseOnClick();
		}
		private void DrawEllipseOnClick()
		{
			dialogProcessor.AddEllipse();


			viewPort.Invalidate();
		}

        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
			DrawCircleOnClick();

		}

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
			DrawEllipseOnClick();

		}

        private void rectangleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
			dialogProcessor.AddRandomRectangle();

			viewPort.Invalidate();
		}

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
			MakeFigureBigger();
		}
		private void MakeFigureBigger()
		{
			if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.Width += 30;
				dialogProcessor.Selection.Height += 30;
			}
			base.Invalidate();
		}

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
			MakeFigureSmaller();
		}
		private void MakeFigureSmaller()
		{
			if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.Width -= 20;
				dialogProcessor.Selection.Height -= 20;
			}
			base.Invalidate();
		}

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
			DeleteFigure();
			base.Invalidate();
		}
		public void DeleteFigure()
		{

			dialogProcessor.ShapeList.Remove(dialogProcessor.Selection);

			base.Invalidate();
		}

        private void makeBiggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
			MakeFigureBigger();
        }

        private void makeSmallerToolStripMenuItem_Click(object sender, EventArgs e)
        {
			MakeFigureSmaller();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
			DeleteFigure();
			base.Invalidate();
		}

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
			ChangeStrokeColorOfFigure();
		}
		private void ChangeStrokeColorOfFigure()
		{
			if (colorDialog1.ShowDialog() == DialogResult.OK)
			{
				dialogProcessor.Selection.StrokeColor = colorDialog1.Color;

			}
			viewPort.Invalidate();
		}

        private void borderColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
			ChangeStrokeColorOfFigure();
		}

        private void copyAsRectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
			int widthOfCopiedFigure = (int)dialogProcessor.Selection.Width;
			int heightOfCopiedFigure = (int)dialogProcessor.Selection.Height;

			Color fillColorOfCopiedFigure = dialogProcessor.Selection.FillColor;
			Color strokeColorOfCopiedFigure = dialogProcessor.Selection.StrokeColor;

			string name = dialogProcessor.Selection.Name;
			string group = dialogProcessor.Selection.Group;

			dialogProcessor.CopyAndAddRectangle(widthOfCopiedFigure, heightOfCopiedFigure, fillColorOfCopiedFigure, strokeColorOfCopiedFigure, name, group);

			viewPort.Invalidate();

			base.Invalidate();
		}

        private void copyCircleToolStripMenuItem_Click(object sender, EventArgs e)
        {
			int widthOfCopiedFigure = (int)dialogProcessor.Selection.Width;
			int heightOfCopiedFigure = (int)dialogProcessor.Selection.Height;

			Color fillColorOfCopiedFigure = dialogProcessor.Selection.FillColor;
			Color strokeColorOfCopiedFigure = dialogProcessor.Selection.StrokeColor;

			string name = dialogProcessor.Selection.Name;
			string group = dialogProcessor.Selection.Group;

			dialogProcessor.CopyAndAddCircle(widthOfCopiedFigure, heightOfCopiedFigure, fillColorOfCopiedFigure, strokeColorOfCopiedFigure, name, group);


			viewPort.Invalidate();

			base.Invalidate();
		}

        private void viewPort_Load(object sender, EventArgs e)
        {
			base.Invalidate();
		}

        private void speedMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
			dialogProcessor.Save();

			base.Invalidate();

			statusBar.Items[0].Text = "Последно действие: Сериализация";
		}


        private void toolStripButton9_Click_1(object sender, EventArgs e)
        {
			dialogProcessor.Save();

			base.Invalidate();

			
		}

        private void toolStripButton10_Click_1(object sender, EventArgs e)
        {
			dialogProcessor.Load();

			base.Invalidate();

		}

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
			dialogProcessor.Save();

			base.Invalidate();
		}

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
			dialogProcessor.Load();

			base.Invalidate();
		}

        private void makeBiggerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
			MakeFigureBigger();
        }

        private void makeSmallerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
			MakeFigureSmaller();
        }

        private void randomColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
			ChangeFillColorRandomly();
			ChangeStrokeColorRandomly();
		}

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
			DeleteFigure();
			base.Invalidate();
		}

        private void borderColorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
			ChangeStrokeColorOfFigure();
		}
    }
}
