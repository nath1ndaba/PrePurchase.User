using MudBlazor;

namespace PrePurchase.Mobile.Shared.Theme;

public class PrePurchaseTheme
{
    public static MudTheme Theme => new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#4DB8E8",
            Secondary = "#2E8AB8",
            Success = "#4CAF50",
            Info = "#2196F3",
            Warning = "#FF9800",
            Error = "#F44336",
            Dark = "#1A237E",
            TextPrimary = "#212121",
            TextSecondary = "#757575",
            Background = "#FFFFFF",
            Surface = "#F5F5F5",
            AppbarBackground = "#4DB8E8",
            DrawerBackground = "#FFFFFF"
        },

        PaletteDark = new PaletteDark
        {
            Primary = "#4DB8E8",
            Secondary = "#2E8AB8",
            Success = "#4CAF50",
            Info = "#2196F3",
            Warning = "#FF9800",
            Error = "#F44336",
            Dark = "#0D1117",
            TextPrimary = "#FFFFFF",
            TextSecondary = "#B0B0B0",
            Background = "#161B22",
            Surface = "#21262D",
            AppbarBackground = "#21262D",
            DrawerBackground = "#161B22"
        },

        LayoutProperties = new LayoutProperties
        {
            DefaultBorderRadius = "8px"
        },

        Typography = new Typography
        {
            Default = new DefaultTypography
            {
                FontFamily = new[] { "Roboto", "Helvetica", "Arial", "sans-serif" }
            },
            H1 = new H1Typography
            {
                FontSize = "2.5rem",
                FontWeight = "500"
            },
            H2 = new H2Typography
            {
                FontSize = "2rem",
                FontWeight = "500"
            },
            H3 = new H3Typography
            {
                FontSize = "1.75rem",
                FontWeight = "500"
            },
            H4 = new H4Typography
            {
                FontSize = "1.5rem",
                FontWeight = "500"
            },
            H5 = new H5Typography
            {
                FontSize = "1.25rem",
                FontWeight = "500"
            },
            H6 = new H6Typography
            {
                FontSize = "1rem",
                FontWeight = "500"
            },
            Button = new ButtonTypography
            {
                FontWeight = "500",
                TextTransform = "none"
            }
        }
    };
}