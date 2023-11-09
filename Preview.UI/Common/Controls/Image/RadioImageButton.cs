using System.Windows.Controls;
using System.Windows.Media;

namespace Xylia.Preview.UI.Common.Controls;
public class RadioImageButton : RadioButton
{
    public ImageSource BackgrondImage { get; set; }

    public static readonly DependencyProperty BackgrondImageProperty =
        DependencyProperty.Register(nameof(BackgrondImage), typeof(ImageSource), typeof(RadioImageButton));
}