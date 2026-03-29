using Godot;
using System;

public partial class GridOverlay : Node2D
{
	[Export] public int CellSize { get; set; } = 16;
	[Export] public int Columns { get; set; } = 16;
	[Export] public int Rows { get; set; } = 16;
	[Export] public Color LineColor { get; set; } = new Color(1, 1, 1, 0.4f);
	[Export] public float LineWidth { get; set; } = 1f;

	public override void _Ready()
	{
		// No per-frame processing needed for a static grid overlay.
		// QueueRedraw() ensures _Draw() is called once (or on demand).
		QueueRedraw();
	}

	public override void _Draw()
	{
		if (CellSize <= 0 || Columns <= 0 || Rows <= 0)
			return;

		// Draw vertical grid lines
		for (int x = 0; x <= Columns; x++)
		{
			var start = new Vector2(x * CellSize, 0);
			var end = new Vector2(x * CellSize, Rows * CellSize);
			DrawLine(start, end, LineColor, LineWidth);
		}

		// Draw horizontal grid lines
		for (int y = 0; y <= Rows; y++)
		{
			var start = new Vector2(0, y * CellSize);
			var end = new Vector2(Columns * CellSize, y * CellSize);
			DrawLine(start, end, LineColor, LineWidth);
		}
	}

	public void Configure(int cellSize, int columns, int rows, Color? lineColor = null, float? lineWidth = null)
	{
		CellSize = cellSize;
		Columns = columns;
		Rows = rows;

		if (lineColor.HasValue)
			LineColor = lineColor.Value;

		if (lineWidth.HasValue)
			LineWidth = lineWidth.Value;

		QueueRedraw();
	}
}
