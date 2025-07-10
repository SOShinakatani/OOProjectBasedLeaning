using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace OOProjectBasedLeaning
{
    public abstract class DragDropPanel : Panel, ISerializable
    {
        private Form form = NullDragDropForm.Instance;

        protected DragDropPanel()
        {
            MouseDown += (sender, e) => OnPanelMouseDown();
        }

        /// <summary>
        /// Override this method to define behavior when the panel is clicked.
        /// Example:
        /// protected override void OnPanelMouseDown()
        /// {
        ///     DoDragDropCopy();
        /// }
        /// </summary>
        protected abstract void OnPanelMouseDown();

        protected void DoDragDropCopy() => DoDragDrop(this, DragDropEffects.Copy);
        protected void DoDragDropMove() => DoDragDrop(this, DragDropEffects.Move);

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Implement serialization logic if needed
        }

        public virtual DragDropPanel AddDragDropForm(Form form, Point dropPoint)
        {
            RemoveForm();
            this.form = form;
            this.form.Controls.Add(MoveTo(dropPoint));
            return this;
        }

        public virtual DragDropPanel RemoveForm()
        {
            if (form.Contains(this))
            {
                form.Controls.Remove(this);
                form = NullDragDropForm.Instance;
            }
            return this;
        }

        protected DragDropPanel MoveTo(Point point)
        {
            Location = point;
            return this;
        }
    }

    public class NullDragDropPanel : DragDropPanel, NullObject
    {
        private static readonly DragDropPanel instance = new NullDragDropPanel();

        private NullDragDropPanel() { }

        public static DragDropPanel Instance => instance;

        protected override void OnPanelMouseDown() { }

        public override DragDropPanel AddDragDropForm(Form form, Point dropPoint) => this;
        public override DragDropPanel RemoveForm() => this;
    }
}
