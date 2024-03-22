using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Revit_WTBG
{
    /// <summary>
    /// Add.xaml 的交互逻辑
    /// </summary>
    public partial class Add : Window
    {
        public List<ProblemReport> problemReports = new List<ProblemReport>();
        private Add()
        {
            InitializeComponent();
          
        }

        private static Add _instance;
        public static Add Instance
        {
            get
            {
                if (ReferenceEquals(_instance, null))
                {
                    _instance = new Add();
                }
                return _instance;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        #region 清空add页面中的所有textbox中的值
        /// <summary>
        /// 清空add页面中的所有textbox中的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="depObj"></param>
        /// <returns></returns>
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        #endregion
        private void tBoxQuestion_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void tBoxAdvice_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void tBoxReason_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void tBoxImportanceLevel_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void tBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            SysCache.Instance.Name = Instance.tBoxName.Text;
        }

        private void tBoxRange_TextChanged(object sender, TextChangedEventArgs e)
        {
            SysCache.Instance.Range = Instance.tBoxRange.Text;
        }

        private void tBoxHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            SysCache.Instance.Height = Instance.tBoxHeight.Text;
        }

        private void tBoxAxis_TextChanged(object sender, TextChangedEventArgs e)
        {
            SysCache.Instance.Axis = Instance.tBoxAxis.Text;
        }

        private void tBoxElevation_TextChanged(object sender, TextChangedEventArgs e)
        {
            SysCache.Instance.Elevation = Instance.tBoxElevation.Text;
        }

        private void tBoxModelName_TextChanged(object sender, TextChangedEventArgs e)
        {
            SysCache.Instance.ModelName = Instance.tBoxModelName.Text;
        }

        private void tBoxDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            SysCache.Instance.Date = Instance.tBoxDate.Text;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Add.Instance.Close();
        }

        private void cbBoxState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Instance.cbBoxState.SelectedValue != null)
            {
                SysCache.Instance.State = Instance.cbBoxState.SelectedValue.ToString();
            }

        }

        private void cbBoxSpeciality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Instance.cbBoxSpeciality.SelectedValue != null)
            {
                SysCache.Instance.Speciality = Instance.cbBoxSpeciality.SelectedValue.ToString();
            }

        }

        private void cbBoxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Instance.cbBoxType.SelectedValue != null)
            {
                SysCache.Instance.Type = Instance.cbBoxType.SelectedValue.ToString();
            }

        }
    }
}
