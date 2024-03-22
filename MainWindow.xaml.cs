using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Revit_WTBG
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        private MainWindow()
        {
            InitializeComponent();
            //状态
            List<string> states = new List<string>() { "全部", "待回复", "待核查", "完成" };
            cbBoxStateSearch.ItemsSource = states;
            cbBoxStateSearch.SelectedIndex = 0;
            //专业
            List<string> speciality = new List<string>() { "全部", "建筑", "结构", "土建综合", "暖通", "给排水", "电气", "燃气", "机电综合", "幕墙", "精装", "景观", "市政" };
            cbBoxSpecialitySearch.ItemsSource = speciality;
            cbBoxSpecialitySearch.SelectedIndex = 0;
            //类型
            List<string> types = new List<string>() { "全部", "图纸类", "立管、管井类", "标高类", "附件类", "设备类", "套管类", "维修空间类", "净高类", "留洞类", "管线优化类", "碰撞类", "其他" };
            cbBoxTypeSearch.ItemsSource = types;
            cbBoxTypeSearch.SelectedIndex = 0;
        }

        private static MainWindow _instance;
        public static MainWindow Instance
        {
            get
            {
                if (ReferenceEquals(_instance, null))
                {
                    _instance = new MainWindow();
                }
                return _instance;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //AddStackPanel();
            Add.Instance.tBoxModelName.Text = SysCache.Instance.ModelName;
            Add.Instance.cbBoxState.SelectedIndex = -1;
            Add.Instance.cbBoxType.SelectedIndex = -1;
            Add.Instance.cbBoxSpeciality.SelectedIndex = -1;
            // 创建并显示新窗口
            Add.Instance.Show();

        }

        /// <summary>
        /// 赋值给问题列表
        /// </summary>
        /// <param name="add"></param>
        /// <param name="problemReport"></param>
        /// <returns></returns>
        public static ProblemReport AssignmentToProblemReport(Add add, ProblemReport problemReport)
        {
            problemReport.QuestionDescribe = add.tBoxQuestion.Text;
            problemReport.OptimizationSuggestions = add.tBoxAdvice.Text;
            problemReport.BasisInvolved = add.tBoxReason.Text;
            problemReport.State = add.cbBoxState.SelectedValue.ToString();
            problemReport.ImportanceLevel = add.tBoxImportanceLevel.Text;
            problemReport.Speciality = add.cbBoxSpeciality.SelectedValue.ToString();
            problemReport.Type = add.cbBoxType.SelectedValue.ToString();
            problemReport.Name = add.tBoxName.Text;
            problemReport.Range = add.tBoxRange.Text;
            problemReport.Height = add.tBoxHeight.Text;
            problemReport.Axis = add.tBoxAxis.Text;
            problemReport.Elevation = add.tBoxElevation.Text;
            problemReport.ModelName = add.tBoxModelName.Text;
            problemReport.Date = add.tBoxDate.Text;

            return problemReport;
        }
        /// <summary>
        /// 赋值给Add窗口
        /// </summary>
        /// <param name="problemReport"></param>
        /// <param name="add"></param>
        /// <returns></returns>
        public static Add AssignmentToAddWindow(ProblemReport problemReport, Add add)
        {
            
        }
        public void AddStackPanel(ProblemReport problemReport)
        {
            // 创建一个新的 StackPanel
            StackPanel newStackPanel = new StackPanel();

            // 设置一些样式或属性，如果需要的话
            newStackPanel.Margin = new Thickness(5);
            newStackPanel.Orientation = Orientation.Horizontal;
            newStackPanel.Background = Brushes.White;
            //添加新的StackPanel
            StackPanel newLeftStackPanel = new StackPanel();
            StackPanel newRightStackPanel = new StackPanel();

            Image image = new Image();
            image.Width = 100;
            image.Height = 100;
            // 创建一个 Border 控件，并将 Image 作为其子元素
            Border border = new Border();
            border.BorderBrush = Brushes.Black; // 设置边框颜色为黑色
            border.BorderThickness = new Thickness(2); // 设置边框厚度为2像素
            border.Child = image; // 设置 Image 为 Border 的子元素
            TextBlock tbViewName = new TextBlock();
            tbViewName.Text = "问题描述：" + problemReport.QuestionDescribe;//问题描述
            newLeftStackPanel.Children.Add(border);
            newLeftStackPanel.Children.Add(tbViewName);

            TextBlock tbBlueprintName = new TextBlock();
            tbBlueprintName.Text = "模型名称：" + problemReport.ModelName;//模型名称
            TextBlock tbReplyName = new TextBlock();
            tbReplyName.Text = "状态：" + problemReport.State;//状态
            TextBlock tbCategoryName = new TextBlock();
            tbCategoryName.Text = "专业：" + problemReport.Speciality;//专业
            TextBlock tbTimeName = new TextBlock();
            DateTime currentTime = DateTime.Now;
            tbTimeName.Text = currentTime.ToString("yyyy-MM-dd");
            TextBlock tbID = new TextBlock();
            tbID.Visibility = Visibility.Collapsed;
            tbID.Text = problemReport.ID.ToString();
            Button btnDel = new Button();
            btnDel.Content = "删除";
            btnDel.Click += btnDel_Click;
            newRightStackPanel.Children.Add(tbBlueprintName);
            newRightStackPanel.Children.Add(tbReplyName);
            newRightStackPanel.Children.Add(tbCategoryName);
            newRightStackPanel.Children.Add(tbTimeName);
            newRightStackPanel.Children.Add(btnDel);

            newStackPanel.Children.Add(newLeftStackPanel);
            newStackPanel.Children.Add(newRightStackPanel);

            newStackPanel.MouseLeftButtonDown += StackPanel_Click;
            // 将新的 StackPanel 添加到主 StackPanel 中
            MainStackPanel.Children.Add(newStackPanel);

            //删除事件
            void btnDel_Click(object sender, RoutedEventArgs e)
            {
                // 获取触发点击事件的按钮
                Button deleteButton = (Button)sender;

                // 获取该按钮的父级 StackPanel
                StackPanel stackPanel1 = (StackPanel)deleteButton.Parent;
                StackPanel stackPanel2 = (StackPanel)stackPanel1.Parent;

                // 从父级容器中移除 StackPanel
                MainStackPanel.Children.Remove(stackPanel2);
                //从问题列表中移除当前项
                Add.Instance.problemReports.RemoveAt(Convert.ToInt32(tbID.Text));
                if (!Add.Instance.problemReports.Equals(null))
                {
                    MainWindow.Instance.tBlockCount.Text = "共" + Add.Instance.problemReports.Count + "条";
                }
            }

            //点击修改
            void StackPanel_Click(object sender, RoutedEventArgs e)
            {

                Add.Instance.Show();
                AssignmentToAddWindow(Add.Instance.problemReports[Convert.ToInt32(tbID.Text)], Add.Instance);
                //Add.Instance.problemReports.RemoveAt(Convert.ToInt32(tbID.Text)-1);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            MainStackPanel.Children.Clear();
            foreach (ProblemReport item in Add.Instance.problemReports)
            {
                AddStackPanel(item);
            }
            if (!Add.Instance.problemReports.Equals(null))
            {
                MainWindow.Instance.tBlockCount.Text = "共" + Add.Instance.problemReports.Count + "条";
            }
        }

        private void cbBoxSpecialitySearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter(Add.Instance.problemReports);
        }

        private void cbBoxTypeSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter(Add.Instance.problemReports);
        }

        private void cbBoxStateSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter(Add.Instance.problemReports);
        }

        private void tBoxSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            cbBoxTypeSearch.SelectedIndex = 0;
            cbBoxSpecialitySearch.SelectedIndex = 0;
            cbBoxStateSearch.SelectedIndex = 0;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

            MainStackPanel.Children.Clear();
            List<ProblemReport> targetProblemReports = new List<ProblemReport>();
            targetProblemReports = Add.Instance.problemReports.Where(
                item => item.QuestionDescribe.Contains(tBoxSearch.Text)).ToList();
            //将符合条件的筛选出来
            foreach (ProblemReport item in targetProblemReports)
            {
                AddStackPanel(item);
            }
        }


        #region 按条件过滤
        /// <summary>
        /// 按条件过滤
        /// </summary>
        /// <param name="problemReports"></param>
        public void Filter(List<ProblemReport> problemReports)
        {
            MainStackPanel.Children.Clear();
            tBoxSearch.Clear();
            List<ProblemReport> targetProblemReports = new List<ProblemReport>();
            if (problemReports.Count != 0)
            {
                if (cbBoxSpecialitySearch.SelectedItem.ToString() != "全部"
                    && cbBoxStateSearch.SelectedItem.ToString() != "全部"
                    && cbBoxTypeSearch.SelectedItem.ToString() != "全部")
                {
                    targetProblemReports = problemReports.Where(
                        item => item.Speciality == cbBoxSpecialitySearch.SelectedItem.ToString()
                        && item.State == cbBoxStateSearch.SelectedItem.ToString()
                        && item.Type == cbBoxTypeSearch.SelectedItem.ToString()).ToList();
                }
                else if (cbBoxSpecialitySearch.SelectedItem.ToString() == "全部"
                    && cbBoxStateSearch.SelectedItem.ToString() != "全部"
                    && cbBoxTypeSearch.SelectedItem.ToString() != "全部")
                {
                    targetProblemReports = problemReports.Where(
                        item => item.State == cbBoxStateSearch.SelectedItem.ToString()
                        && item.Type == cbBoxTypeSearch.SelectedItem.ToString()).ToList();
                }
                else if (cbBoxSpecialitySearch.SelectedItem.ToString() != "全部"
                    && cbBoxStateSearch.SelectedItem.ToString() == "全部"
                    && cbBoxTypeSearch.SelectedItem.ToString() != "全部")
                {
                    targetProblemReports = problemReports.Where(
                        item => item.Speciality == cbBoxSpecialitySearch.SelectedItem.ToString()
                        && item.Type == cbBoxTypeSearch.SelectedItem.ToString()).ToList();
                }
                else if (cbBoxSpecialitySearch.SelectedItem.ToString() != "全部"
                    && cbBoxStateSearch.SelectedItem.ToString() != "全部"
                    && cbBoxTypeSearch.SelectedItem.ToString() == "全部")
                {
                    targetProblemReports = problemReports.Where(
                        item => item.Speciality == cbBoxSpecialitySearch.SelectedItem.ToString()
                        && item.State == cbBoxStateSearch.SelectedItem.ToString()).ToList();
                }
                else if (cbBoxSpecialitySearch.SelectedItem.ToString() != "全部"
                    && cbBoxStateSearch.SelectedItem.ToString() == "全部"
                    && cbBoxTypeSearch.SelectedItem.ToString() == "全部")
                {
                    targetProblemReports = problemReports.Where(
                        item => item.Speciality == cbBoxSpecialitySearch.SelectedItem.ToString()).ToList();
                }
                else if (cbBoxSpecialitySearch.SelectedItem.ToString() == "全部"
                    && cbBoxStateSearch.SelectedItem.ToString() != "全部"
                    && cbBoxTypeSearch.SelectedItem.ToString() == "全部")
                {
                    targetProblemReports = problemReports.Where(
                        item => item.State == cbBoxStateSearch.SelectedItem.ToString()).ToList();
                }
                else if (cbBoxSpecialitySearch.SelectedItem.ToString() == "全部"
                    && cbBoxStateSearch.SelectedItem.ToString() == "全部"
                    && cbBoxTypeSearch.SelectedItem.ToString() != "全部")
                {
                    targetProblemReports = problemReports.Where(
                        item => item.Type == cbBoxTypeSearch.SelectedItem.ToString()).ToList();
                }
                else
                {
                    targetProblemReports = problemReports;
                }


                //更新计数
                if (!targetProblemReports.Equals(null))
                {
                    MainWindow.Instance.tBlockCount.Text = "共" + targetProblemReports.Count + "条";
                }
                else
                {
                    MainWindow.Instance.tBlockCount.Text = "共0条";
                }

                //将符合条件的筛选出来
                foreach (ProblemReport item in targetProblemReports)
                {
                    AddStackPanel(item);
                }
            }
        }

        #endregion


    }
}
