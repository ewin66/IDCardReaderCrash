# IDCardReaderCrash
此项目重现了一个WPF的bug：当成功读取身份证后，点击含有DataGrid的TabItem时，会引发DivideByZeroException异常（或者其他异常）


异常类型1：
发生了 System.Windows.Markup.XamlParseException
  _HResult=-2146233087
  _message=初始化“System.Windows.Controls.Primitives.ScrollBar”时引发了异常。
  HResult=-2146233087
  IsTransient=false
  Message=初始化“System.Windows.Controls.Primitives.ScrollBar”时引发了异常。
  Source=PresentationFramework
  LineNumber=0
  LinePosition=0
  StackTrace:
       在 System.Windows.FrameworkTemplate.LoadTemplateXaml(XamlReader templateReader, XamlObjectWriter currentWriter)
  InnerException: System.ArithmeticException
       _HResult=-2147024362
       _message=算术运算中发生溢出或下溢。
       HResult=-2147024362
       IsTransient=false
       Message=算术运算中发生溢出或下溢。
       Source=mscorlib
       StackTrace:
            在 System.Double.Equals(Object obj)
            在 System.Object.Equals(Object objA, Object objB)
            在 System.Windows.DependencyObject.UpdateEffectiveValue(EntryIndex entryIndex, DependencyProperty dp, PropertyMetadata metadata, EffectiveValueEntry oldEntry, EffectiveValueEntry& newEntry, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType)
            在 System.Windows.StyleHelper.ApplyStyleOrTemplateValue(FrameworkObject fo, DependencyProperty dp)
            在 System.Windows.StyleHelper.InvalidateContainerDependents(DependencyObject container, FrugalStructList`1& exclusionContainerDependents, FrugalStructList`1& oldContainerDependents, FrugalStructList`1& newContainerDependents)
            在 System.Windows.StyleHelper.DoThemeStyleInvalidations(FrameworkElement fe, FrameworkContentElement fce, Style oldThemeStyle, Style newThemeStyle, Style style)
            在 System.Windows.StyleHelper.UpdateThemeStyleCache(FrameworkElement fe, FrameworkContentElement fce, Style oldThemeStyle, Style newThemeStyle, Style& themeStyleCache)
            在 System.Windows.FrameworkElement.OnThemeStyleChanged(DependencyObject d, Object oldValue, Object newValue)
            在 System.Windows.StyleHelper.GetThemeStyle(FrameworkElement fe, FrameworkContentElement fce)
            在 System.Windows.FrameworkElement.UpdateThemeStyleProperty()
            在 System.Windows.FrameworkElement.OnInitialized(EventArgs e)
            在 System.Windows.FrameworkElement.TryFireInitialized()
            在 System.Windows.FrameworkElement.EndInit()
            在 MS.Internal.Xaml.Runtime.ClrObjectRuntime.InitializationGuard(XamlType xamlType, Object obj, Boolean begin)
       InnerException: 
