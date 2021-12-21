using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fyaazLab2
{
    public class MainModel : ViewModel
    {
        private ViewModel _viewModel;
        public DDBOperations db = new DDBOperations();

        public Login cView;

        public ViewModel viewModel
        {
            get { return _viewModel; }
            set
            {
                SetProperty(ref _viewModel, value);
            }
        }

        public MainModel()
        {
            cView = new Login();
            cView.mwvm = this;

            viewModel = cView;
        }

    }
}
