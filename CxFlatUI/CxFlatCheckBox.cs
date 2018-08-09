﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;


public class CxFlatCheckBox :CheckBox
{
    #region 变量
    Color EnabledCheckedColor;
    Color EnabledUnCheckedColor = ColorTranslator.FromHtml("#9c9ea1");
    Color DisabledColor = ColorTranslator.FromHtml("#c4c6ca");
    Color EnabledStringColor = ColorTranslator.FromHtml("#999999");
    Color DisabledStringColor = ColorTranslator.FromHtml("#babbbd");
    Timer AnimationTimer = new Timer { Interval = 15 };
    int SizeAnimationNum = 14;
    int PointAnimationNum = 3;
    bool enterFlag = false;
    #endregion

    #region 属性
    private Color _checkedColor = Color.Blue;
    public Color CheckedColor
    {
        get { return _checkedColor; }
        set
        {
            _checkedColor = value;
            Invalidate();
        }
    }
    #endregion

    #region 事件
    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        AnimationTimer.Start();
    }

    protected override void OnResize(EventArgs e)
    {
        Height = 20;
        Width = 25 + (int)CreateGraphics().MeasureString(Text, Font).Width;
    }

    protected override void OnMouseEnter(EventArgs eventargs)
    {
        base.OnMouseEnter(eventargs);
        enterFlag = true;
        Invalidate();
    }

    protected override void OnMouseLeave(EventArgs eventargs)
    {
        base.OnMouseLeave(eventargs);
        enterFlag = false;
        Invalidate();
    }
    #endregion

    protected override void OnPaint(PaintEventArgs pevent)
    {
        Graphics graphics = pevent.Graphics;
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        graphics.Clear(Color.White);

        var checkmarkPath = DrawHelper.CreateRoundRect(2, 2, 16, 16, 1);
        var checkMarkLine = new Rectangle(3, 3, 14, 14);

        EnabledCheckedColor = _checkedColor;
        SolidBrush BG = new SolidBrush(Enabled ? (Checked || enterFlag ? EnabledCheckedColor : EnabledUnCheckedColor) : DisabledColor);
        Pen Pen = new Pen(BG.Color);

        graphics.FillPath(BG, checkmarkPath);
        graphics.DrawPath(Pen, checkmarkPath);

        //绘制对号
        graphics.DrawLines(new Pen(Color.White, 2), new PointF[]
        {
            new PointF(5, 9),new PointF(9, 13), new PointF(15, 6)
        });
        graphics.FillRectangle(new SolidBrush(Color.White), PointAnimationNum, PointAnimationNum, SizeAnimationNum, SizeAnimationNum);

        //绘制文字
        graphics.DrawString(Text, Font, new SolidBrush(Color.Black), new RectangleF(22, 0, Width - 22, Height), new StringFormat
        {
            LineAlignment = StringAlignment.Center
        });
    }

    //
    //选择动画
    //
    private void AnimationTick(object sender, EventArgs e)
    {
        if (Checked)
        {
            if (SizeAnimationNum > 0)
            {
                SizeAnimationNum -= 2;
                PointAnimationNum += 1;
                Invalidate();
            }
        }
        else
        {
            if (SizeAnimationNum < 14)
            {
                SizeAnimationNum += 2;
                PointAnimationNum -= 1;
                Invalidate();
            }
        }
    }

    public CxFlatCheckBox()
    {
        AnimationTimer.Tick += new EventHandler(AnimationTick);
        DoubleBuffered = true;
        Font = new Font("Segoe UI", 12);
    }
}

