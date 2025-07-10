using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace OOProjectBasedLeaning
{
    /// <summary>
    /// ドラッグ＆ドロップ可能なパネルの抽象クラス
    /// </summary>
    public abstract class DragDropPanel : Panel, ISerializable
    {
        private Form currentForm = NullDragDropForm.Instance;

        protected DragDropPanel()
        {
            MouseDown += HandleMouseDown;
        }

        /// <summary>
        /// パネルがクリックされたときの処理を定義する抽象メソッド。
        /// 継承先でオーバーライドして使用してください。
        /// 例:
        /// protected override void OnPanelMouseDown()
        /// {
        ///     DoDragDropCopy();
        /// }
        /// </summary>
        protected abstract void OnPanelMouseDown();

        /// <summary>
        /// ドラッグ＆ドロップ（コピー）を開始する
        /// </summary>
        protected void DoDragDropCopy() => DoDragDrop(this, DragDropEffects.Copy);

        /// <summary>
        /// ドラッグ＆ドロップ（移動）を開始する
        /// </summary>
        protected void DoDragDropMove() => DoDragDrop(this, DragDropEffects.Move);

        /// <summary>
        /// シリアライズ処理（必要に応じて実装）
        /// </summary>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // 必要に応じてシリアライズ処理を実装
        }

        /// <summary>
        /// 指定されたフォームにパネルを追加し、指定位置に移動する
        /// </summary>
        public virtual DragDropPanel AddToForm(Form form, Point dropPoint)
        {
            RemoveFromForm();
            currentForm = form;
            currentForm.Controls.Add(MoveTo(dropPoint));
            return this;
        }

        /// <summary>
        /// 現在のフォームからパネルを削除する
        /// </summary>
        public virtual DragDropPanel RemoveFromForm()
        {
            if (currentForm.Contains(this))
            {
                currentForm.Controls.Remove(this);
                currentForm = NullDragDropForm.Instance;
            }
            return this;
        }

        /// <summary>
        /// パネルを指定位置に移動する
        /// </summary>
        protected DragDropPanel MoveTo(Point point)
        {
            Location = point;
            return this;
        }

        /// <summary>
        /// MouseDownイベントのハンドラ
        /// </summary>
        private void HandleMouseDown(object sender, MouseEventArgs e)
        {
            OnPanelMouseDown();
        }
    }

    /// <summary>
    /// Nullオブジェクトパターンを使用したドラッグ＆ドロップパネルの実装
    /// </summary>
    public class NullDragDropPanel : DragDropPanel, NullObject
    {
        private static readonly DragDropPanel instance = new NullDragDropPanel();

        private NullDragDropPanel() { }

        public static DragDropPanel Instance => instance;

        protected override void OnPanelMouseDown() { }

        public override DragDropPanel AddToForm(Form form, Point dropPoint) => this;
        public override DragDropPanel RemoveFromForm() => this;
    }
}
