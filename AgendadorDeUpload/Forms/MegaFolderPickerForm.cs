using System;
using System.Linq;
using System.Windows.Forms;
using CG.Web.MegaApiClient;

namespace AgendadorDeUpload.Forms
{
    public partial class MegaFolderPickerForm : Form
    {
        private TreeView treeView;
        private Button btnOk;
        private Button btnCancel;
        private readonly INode[] _nodes;

        public string SelectedFolderName { get; private set; }

        public MegaFolderPickerForm(INode[] nodes)
        {
            _nodes = nodes;
            InitializeComponent();
            BuildTree();
        }

        private void InitializeComponent()
        {
            this.treeView = new TreeView();
            this.btnOk = new Button();
            this.btnCancel = new Button();

            this.SuspendLayout();

            this.treeView.Location = new System.Drawing.Point(12, 12);
            this.treeView.Size = new System.Drawing.Size(360, 320);
            this.treeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.treeView.ShowRootLines = true;
            this.treeView.AfterSelect += TreeView_AfterSelect;

            this.btnOk.Text = "OK";
            this.btnOk.Location = new System.Drawing.Point(216, 345);
            this.btnOk.Size = new System.Drawing.Size(75, 28);
            this.btnOk.DialogResult = DialogResult.OK;
            this.btnOk.Enabled = false;

            this.btnCancel.Text = "Cancelar";
            this.btnCancel.Location = new System.Drawing.Point(297, 345);
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.DialogResult = DialogResult.Cancel;

            this.ClientSize = new System.Drawing.Size(384, 385);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Selecionar Pasta Mega";
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);

            this.ResumeLayout(false);
        }

        private void BuildTree()
        {
            var root = _nodes.FirstOrDefault(n => n.Type == NodeType.Root);
            if (root != null)
            {
                var rootNode = treeView.Nodes.Add("Raiz");
                rootNode.Tag = root;
                AddChildNodes(rootNode, root.Id);
                rootNode.Expand();
            }
        }

        private void AddChildNodes(TreeNode parentNode, string parentId)
        {
            foreach (var child in _nodes.Where(n =>
                n.Type == NodeType.Directory && string.Equals(n.ParentId, parentId, StringComparison.Ordinal)))
            {
                var node = parentNode.Nodes.Add(child.Name);
                node.Tag = child;
                AddChildNodes(node, child.Id);
            }
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            btnOk.Enabled = e.Node?.Tag is INode node && node.Type == NodeType.Directory;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK && treeView.SelectedNode?.Tag is INode sel)
            {
                if (sel.Type == NodeType.Root)
                    SelectedFolderName = "";
                else
                    SelectedFolderName = sel.Name;
            }
            base.OnFormClosing(e);
        }
    }
}
