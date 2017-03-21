﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MiP.Ruler.Annotations;

namespace MiP.Ruler
{
    public class RulerLineDisplay : Canvas, INotifyPropertyChanged
    {
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            nameof(Orientation), typeof(Orientation), typeof(RulerLineDisplay), new PropertyMetadata(Orientation.Horizontal, (o, args) => { ((RulerLineDisplay) o)?.DirectionChanged(); }));

        private readonly RulerLine[] _currentLineInArray = new RulerLine[1];
        private readonly List<RulerLine> _rulerLines = new List<RulerLine>();

        public bool ShowPercentages { get; set; } = false;

        private RulerLine _currentLine;
        private readonly Config _config;

        public RulerLineDisplay()
        {
            _config = Config.Instance;

            Initialize();

            MouseMove += MouseMoveHandler;
            SizeChanged += SizeChangedHandler;
        }

        private void Initialize()
        {
            _currentLineInArray[0] = _currentLine = new RulerLine(this, new Point(-100, -100));
        }
        
        public Orientation Orientation
        {
            get { return (Orientation) GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }
        
        public void AddNewRulerLine(Point position)
        {
            _rulerLines.Add(new RulerLine(this, position));
        }

        public void ClearRulerLines()
        {
            foreach (var line in _rulerLines)
                line.RemoveFromDisplay();

            _rulerLines.Clear();
        }

        public void SetCurrentVisible(bool visible)
        {
            _currentLine.SetVisible(visible);
        }

        public void RemoveLast()
        {
            if (_rulerLines.Count > 0)
                _rulerLines[_rulerLines.Count - 1].RemoveFromDisplay();
        }
        
        public void TogglePercentages()
        {
            ShowPercentages = !ShowPercentages;

            foreach (var line in _rulerLines)
                line.RefreshText();

            _currentLine.RefreshText();
        }

        public void ParentSizeChanged()
        {
            foreach (var line in _rulerLines)
                line.RefreshText();
        }

        private void SizeChangedHandler(object sender, SizeChangedEventArgs e)
        {
            foreach (var line in _rulerLines.Concat(_currentLineInArray))
                line.ParentResized();
        }

        private void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            var position = e.GetPosition(this);

            RefreshCurrentRulerLine(position);
        }

        private void DirectionChanged()
        {
            if (_config.ClearLinesOnOrientationChange)
                ClearRulerLines();

            RefreshCurrentRulerLine(Mouse.GetPosition(this));

            foreach (var line in _rulerLines)
                line.ChangeDirection();
        }

        private void RefreshCurrentRulerLine(Point pos)
        {
            _currentLine.MoveLineTo(pos);
            _currentLine.ParentResized();
        }
        
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}