using System;
using System.Windows.Forms;
using IMESAgent.SocketClientEngine;
using System.Data;
using AppClientSuperSocket;
//using IMESServer.Common;

namespace IMESAgent.GUI
{
    public partial class ucConfiguration : UserControl
    {
        private ClientHelper helper = null;
        private bool adding = false;
        private bool updating = false;

        public ucConfiguration()
        {
            InitializeComponent();

            Initialization();
            InitializeControlEvent();
            InitializeRouteComboBox();
            InitializeGrid();

            UpdateControlStatus(true);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style &= ~0x02000000;  // Turn off WS_CLIPCHILDREN 
                return cp;
            }
        }

        private void Initialization()
        {
            PathManager.Initialize(Application.StartupPath);
            helper = ClientHelper.InitializeClientHelper(Application.StartupPath);

            this.DoubleBuffered = true;
            txtInterval.ReadOnly = true;
            txtLogFolder.ReadOnly = true;
        }

        private void InitializeControlEvent()
        {
            cbInspRoute.TextChanged += CbInspRoute_TextChanged;
            cbInspType.TextChanged += CbInspType_TextChanged;

            btnApply.Click += btnApply_Click;

            gridConfigruation.SelectionChanged += GridConfigruation_SelectionChanged;
        }

        private void InitializeRouteComboBox()
        {
            var array = helper.GetInspRoutes();
            cbInspRoute.Items.AddRange(array);
        }

        private void InitializeTypeComboBox(string item)
        {
            ClearComboInfo();

            var array = helper.GetInspTypes(item);
            cbInspType.Items.AddRange(array);
        }

        private void InitializeGrid()
        {
            var table = helper.GatheringPointTable;

            BindToGrid(table);
        }

        private void DisableColumns()
        {
            gridConfigruation.Columns[FieldName.AgentIp].Visible = false;
            gridConfigruation.Columns[FieldName.FileNameFilter].Visible = false;
            Application.DoEvents();
        }

        private void BindToGrid(DataTable table)
        {
            if (table != null)
            {
                gridConfigruation.DataSource = table;
                DisableColumns();
            }
        }

        private void CbInspType_TextChanged(object sender, EventArgs e)
        {
            var cb = sender as ComboBox;

            GetInterval(cbInspRoute.Text, cb.Text);
        }

        private void CbInspRoute_TextChanged(object sender, EventArgs e)
        {
            var cb = sender as ComboBox;

            InitializeTypeComboBox(cb.Text);
        }

        private void GridConfigruation_SelectionChanged(object sender, EventArgs e)
        {
            var grid = sender as DataGridView;

            if (grid.SelectedRows.Count > 0)
            {
                var row = grid.SelectedRows[0];

                if (row != null && row.Cells.Count > 0)
                {
                    SelectedRowIndex = row.Cells[0].Value.ToString();
                }
                SetTextValue(row);
            }

            UpdateControlStatus(true);
        }

        private void btnBroswer_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtLogFolder.Text = dialog.SelectedPath;
                }
            }
        }

        /// <summary>
        ///  the NO must be unique
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (Discard() == true)
                    return;

                if (adding == false)
                {
                    Clear();
                    UpdateControlStatus(false);
                    InitializeRouteComboBox();
                    adding = true;
                }
            }
            catch (Exception ex)
            {
                PopupWindow.ShowDialog(ex.Message, UserMessage.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var grid = gridConfigruation;
            if (grid == null) return;
            if (grid.SelectedRows == null || grid.SelectedRows.Count == 0) return;
            if (Discard() == true) return;

            UpdateControlStatus(false);
            updating = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var grid = gridConfigruation;

                if (Discard() == true) return;
                if (grid == null) return;
                if (grid.SelectedRows == null || grid.SelectedRows.Count == 0) return;

                using (var dialog = new frmPassword())
                {
                    var result = dialog.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        var row = grid.SelectedRows[0];
                        if (row.Cells.Count > 0)
                        {
                            var id = SelectedRowIndex;
                            if (!string.IsNullOrEmpty(id) && helper.GatheringPointTable != null)
                            {
                                for (int i = 0; i < helper.GatheringPointTable.Rows.Count; i++)
                                {
                                    var dataRow = helper.GatheringPointTable.Rows[i];
                                    if (id == dataRow[0].ToString())
                                    {
                                        helper.RemoveGatheringInfo(id, dataRow);
                                        helper.SaveToCsv(PathManager.Instance.GatheringInfoPath, helper.GatheringPointTable);

                                        grid.Refresh();
                                        Clear();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                PopupWindow.ShowDialog(ex.Message, UserMessage.Error);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEmptyTextFeild())
                {
                    if (updating == false && adding == false)
                        return;
                }
                else
                {
                    return;
                }

                using (var dialog = new frmPassword())
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        DataTable tab = null;
                        var alias = txtAlias.Text.TrimEnd();
                        var bcrip = txtBCRIP.Text.TrimEnd();
                        var logPath = txtLogFolder.Text.TrimEnd();
                        var inspRoute = cbInspRoute.Text.TrimEnd();
                        var inspType = cbInspType.Text.TrimEnd();

                        if (adding == true)
                        {
                            var row = helper.GatheringPointTable.Rows.Add();
                            var i = gridConfigruation.Rows.GetLastRow(DataGridViewElementStates.None);

                            gridConfigruation.Rows[i].Selected = true;
                            tab = helper.SaveToDatatable(alias, bcrip, logPath, inspRoute, inspType);
                        }
                        else if (updating == true)
                        {
                            tab = helper.UpdateGatheringInfos(SelectedRowIndex, alias, bcrip, logPath, inspRoute, inspType);
                        }

                        if (adding || updating)
                        {
                            var status = helper.SaveToCsv(PathManager.Instance.GatheringInfoPath, tab);
                            if (status)
                            {
                                PopupWindow.ShowDialog(ClientMessage.ActivateModification, UserMessage.Information);
                                BindToGrid(tab);
                            }
                            else
                            {
                                PopupWindow.ShowDialog(ClientMessage.SaveFailed, UserMessage.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                PopupWindow.ShowDialog(ex.Message, UserMessage.Error);
            }
            finally
            {
                adding = false;
                updating = false;
                UpdateControlStatus(true);
            }
        }

        private bool Discard()
        {
            if ((adding || updating) && PopupWindow.ShowDialog(ClientMessage.Discard, UserMessage.Question) == DialogResult.Yes)
            {
                Clear();
                UpdateControlStatus(true);
                updating = false;
                adding = false;
                return true;
            }
            return false;
        }

        private string SelectedRowIndex
        {
            get;
            set;
        }

        private void ClearComboInfo()
        {
            cbInspType.Items.Clear();
            cbInspType.Text = string.Empty;
            txtInterval.Text = string.Empty;
        }

        private void Clear()
        {
            txtAlias.Text = string.Empty;
            txtBCRIP.Text = string.Empty;
            txtLogFolder.Text = string.Empty;
            txtInterval.Text = string.Empty;

            cbInspRoute.Items.Clear();
            cbInspType.Items.Clear();
            cbInspRoute.Text = string.Empty;
            cbInspType.Text = string.Empty;
        }

        private void SetTextValue(DataGridViewRow row)
        {
            txtAlias.Text = row.Cells[FieldName.Alias].Value.ToString();
            txtBCRIP.Text = row.Cells[FieldName.BcrIp].Value.ToString();
            txtInterval.Text = row.Cells[FieldName.Interval].Value.ToString();
            txtLogFolder.Text = row.Cells[FieldName.LogFolder].Value.ToString();

            InitializeRouteComboBox();
            cbInspRoute.Text = row.Cells[FieldName.ParseGroupName].Value.ToString();

            InitializeTypeComboBox(row.Cells[FieldName.ParseGroupName].Value.ToString());
            cbInspType.Text = row.Cells[FieldName.ParseTypeName].Value.ToString();
        }

        private bool IsEmptyTextFeild()
        {
            if (string.IsNullOrEmpty(txtAlias.Text.TrimEnd())) return true;
            if (string.IsNullOrEmpty(txtBCRIP.Text.TrimEnd())) return true;
            if (string.IsNullOrEmpty(txtInterval.Text.TrimEnd())) return true;
            if (string.IsNullOrEmpty(txtLogFolder.Text.TrimEnd())) return true;
            if (string.IsNullOrEmpty(cbInspRoute.Text)) return true;
            if (string.IsNullOrEmpty(cbInspType.Text)) return true;

            return false;
        }

        private void UpdateControlStatus(bool status)
        {
            txtAlias.ReadOnly = status;
            txtBCRIP.ReadOnly = status;
            cbInspRoute.Enabled = !status;
            cbInspType.Enabled = !status;
        }

        private void GetInterval(string groupName, string typeName)
        {
            var info = helper.GetSpecifiedPaser(groupName, typeName);

            if (info != null)
                txtInterval.Text = info.Interval;
        }
    }
}
