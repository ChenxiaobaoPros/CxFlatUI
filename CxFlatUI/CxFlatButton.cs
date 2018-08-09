﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


public class CxFlatButton : Control
{
    #region 变量
    bool enterFlag = false;
    bool clickFlag = false;
    #endregion

    #region 属性
    private Color _themeColor = Color.RoyalBlue;
    public Color ThemeColor
    {
        get { return _themeColor; }
        set
        {
            _themeColor = value;
            Invalidate();
        }
    }

    private Color _textColor = Color.White;
    public Color TextColor
    {
        get { return _textColor; }
        set
        {
            _textColor = value;
            Invalidate();
        }
    }
    #endregion

    #region 事件
    protected override void OnMouseEnter(EventArgs e)
    {
        base.OnMouseEnter(e);
        enterFlag = true;
        Invalidate();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        enterFlag = false;
        Invalidate();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        clickFlag = true;
        Invalidate();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);
        clickFlag = false;
        Invalidate();
    }
    #endregion

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics graphics = e.Graphics;
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        graphics.Clear(Color.White);

        var BG = DrawHelper.CreateRoundRect(0, 0, Width , Height, 2);
        var brush = new SolidBrush(enterFlag ? (clickFlag ? Color.FromArgb(250, _themeColor) : Color.FromArgb(240, _themeColor)) : _themeColor);
        graphics.FillPath(brush, BG);
        graphics.DrawString(Text, Font, new SolidBrush(Color.White), new RectangleF(0, 0, Width, Height), new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        });
    }


    public CxFlatButton()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        Font = new Font("Segoe UI", 12);
    }

}