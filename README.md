# IDCardReaderCrash
此项目重现了一个WPF的bug：当成功读取身份证后，点击含有DataGrid的TabItem时，会引发DivideByZeroException异常（或者其他异常）

## 环境为 Visual Studio 2013，已验证在windows 10 和windows 7可复现。

####异常类型1：
<pre><code>
未处理System.DivideByZeroException
  _HResult=-2147352558
  _message=尝试除以零。
  HResult=-2147352558
  IsTransient=false
  Message=尝试除以零。
  Source=PresentationFramework
  StackTrace:
       在 System.Windows.Controls.VirtualizingStackPanel.MeasureOverrideImpl(Size constraint, Nullable`1& lastPageSafeOffset, List`1& previouslyMeasuredOffsets, Nullable`1& lastPagePixelSize, Boolean remeasure)
       在 System.Windows.Controls.VirtualizingStackPanel.MeasureOverride(Size constraint)
       在 System.Windows.Controls.Primitives.DataGridRowsPresenter.MeasureOverride(Size constraint)
       在 System.Windows.FrameworkElement.MeasureCore(Size availableSize)
       在 System.Windows.UIElement.Measure(Size availableSize)
       在 MS.Internal.Helper.MeasureElementWithSingleChild(UIElement element, Size constraint)
       在 System.Windows.Controls.ItemsPresenter.MeasureOverride(Size constraint)
       在 System.Windows.FrameworkElement.MeasureCore(Size availableSize)
       在 System.Windows.UIElement.Measure(Size availableSize)
       在 MS.Internal.Helper.MeasureElementWithSingleChild(UIElement element, Size constraint)
       在 System.Windows.Controls.ScrollContentPresenter.MeasureOverride(Size constraint)
       在 System.Windows.FrameworkElement.MeasureCore(Size availableSize)
       在 System.Windows.UIElement.Measure(Size availableSize)
       在 System.Windows.Controls.Grid.MeasureCell(Int32 cell, Boolean forceInfinityV)
       在 System.Windows.Controls.Grid.MeasureCellsGroup(Int32 cellsHead, Size referenceSize, Boolean ignoreDesiredSizeU, Boolean forceInfinityV, Boolean& hasDesiredSizeUChanged)
       在 System.Windows.Controls.Grid.MeasureCellsGroup(Int32 cellsHead, Size referenceSize, Boolean ignoreDesiredSizeU, Boolean forceInfinityV)
       在 System.Windows.Controls.Grid.MeasureOverride(Size constraint)
       在 System.Windows.FrameworkElement.MeasureCore(Size availableSize)
       在 System.Windows.UIElement.Measure(Size availableSize)
       在 System.Windows.Controls.ScrollViewer.MeasureOverride(Size constraint)
       在 System.Windows.FrameworkElement.MeasureCore(Size availableSize)
       在 System.Windows.UIElement.Measure(Size availableSize)
       在 System.Windows.Controls.Border.MeasureOverride(Size constraint)
       在 System.Windows.FrameworkElement.MeasureCore(Size availableSize)
       在 System.Windows.UIElement.Measure(Size availableSize)
       在 System.Windows.Controls.Control.MeasureOverride(Size constraint)
       在 System.Windows.Controls.DataGrid.MeasureOverride(Size availableSize)
       在 System.Windows.FrameworkElement.MeasureCore(Size availableSize)
       在 System.Windows.UIElement.Measure(Size availableSize)
       在 System.Windows.Controls.Grid.MeasureOverride(Size constraint)
       在 System.Windows.FrameworkElement.MeasureCore(Size availableSize)
       在 System.Windows.UIElement.Measure(Size availableSize)
       在 MS.Internal.Helper.MeasureElementWithSingleChild(UIElement element, Size constraint)
       在 System.Windows.Controls.ContentPresenter.MeasureOverride(Size constraint)
       在 System.Windows.FrameworkElement.MeasureCore(Size availableSize)
       在 System.Windows.UIElement.Measure(Size availableSize)
       在 System.Windows.ContextLayoutManager.UpdateLayout()
       在 System.Windows.UIElement.UpdateLayout()
       在 System.Windows.Controls.TabItem.OnPreviewGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
       在 System.Windows.UIElement.OnPreviewGotKeyboardFocusThunk(Object sender, KeyboardFocusChangedEventArgs e)
       在 System.Windows.Input.KeyboardFocusChangedEventArgs.InvokeEventHandler(Delegate genericHandler, Object genericTarget)
       在 System.Windows.RoutedEventArgs.InvokeHandler(Delegate handler, Object target)
       在 System.Windows.RoutedEventHandlerInfo.InvokeHandler(Object target, RoutedEventArgs routedEventArgs)
       在 System.Windows.EventRoute.InvokeHandlersImpl(Object source, RoutedEventArgs args, Boolean reRaised)
       在 System.Windows.UIElement.RaiseEventImpl(DependencyObject sender, RoutedEventArgs args)
       在 System.Windows.UIElement.RaiseTrustedEvent(RoutedEventArgs args)
       在 System.Windows.UIElement.RaiseEvent(RoutedEventArgs args, Boolean trusted)
       在 System.Windows.Input.InputManager.ProcessStagingArea()
       在 System.Windows.Input.InputManager.ProcessInput(InputEventArgs input)
       在 System.Windows.Input.KeyboardDevice.TryChangeFocus(DependencyObject newFocus, IKeyboardInputProvider keyboardInputProvider, Boolean askOld, Boolean askNew, Boolean forceToNullIfFailed)
       在 System.Windows.Input.KeyboardDevice.Focus(DependencyObject focus, Boolean askOld, Boolean askNew, Boolean forceToNullIfFailed)
       在 System.Windows.Input.KeyboardDevice.Focus(IInputElement element)
       在 System.Windows.UIElement.Focus()
       在 System.Windows.Controls.TabItem.SetFocus()
       在 System.Windows.Controls.TabItem.OnMouseLeftButtonDown(MouseButtonEventArgs e)
       在 System.Windows.UIElement.OnMouseLeftButtonDownThunk(Object sender, MouseButtonEventArgs e)
       在 System.Windows.Input.MouseButtonEventArgs.InvokeEventHandler(Delegate genericHandler, Object genericTarget)
       在 System.Windows.RoutedEventArgs.InvokeHandler(Delegate handler, Object target)
       在 System.Windows.RoutedEventHandlerInfo.InvokeHandler(Object target, RoutedEventArgs routedEventArgs)
       在 System.Windows.EventRoute.InvokeHandlersImpl(Object source, RoutedEventArgs args, Boolean reRaised)
       在 System.Windows.UIElement.ReRaiseEventAs(DependencyObject sender, RoutedEventArgs args, RoutedEvent newEvent)
       在 System.Windows.UIElement.OnMouseDownThunk(Object sender, MouseButtonEventArgs e)
       在 System.Windows.Input.MouseButtonEventArgs.InvokeEventHandler(Delegate genericHandler, Object genericTarget)
       在 System.Windows.RoutedEventArgs.InvokeHandler(Delegate handler, Object target)
       在 System.Windows.RoutedEventHandlerInfo.InvokeHandler(Object target, RoutedEventArgs routedEventArgs)
       在 System.Windows.EventRoute.InvokeHandlersImpl(Object source, RoutedEventArgs args, Boolean reRaised)
       在 System.Windows.UIElement.RaiseEventImpl(DependencyObject sender, RoutedEventArgs args)
       在 System.Windows.UIElement.RaiseTrustedEvent(RoutedEventArgs args)
       在 System.Windows.UIElement.RaiseEvent(RoutedEventArgs args, Boolean trusted)
       在 System.Windows.Input.InputManager.ProcessStagingArea()
       在 System.Windows.Input.InputManager.ProcessInput(InputEventArgs input)
       在 System.Windows.Input.InputProviderSite.ReportInput(InputReport inputReport)
       在 System.Windows.Interop.HwndMouseInputProvider.ReportInput(IntPtr hwnd, InputMode mode, Int32 timestamp, RawMouseActions actions, Int32 x, Int32 y, Int32 wheel)
       在 System.Windows.Interop.HwndMouseInputProvider.FilterMessage(IntPtr hwnd, WindowMessage msg, IntPtr wParam, IntPtr lParam, Boolean& handled)
       在 System.Windows.Interop.HwndSource.InputFilterMessage(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, Boolean& handled)
       在 MS.Win32.HwndWrapper.WndProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, Boolean& handled)
       在 MS.Win32.HwndSubclass.DispatcherCallbackOperation(Object o)
       在 System.Windows.Threading.ExceptionWrapper.InternalRealCall(Delegate callback, Object args, Int32 numArgs)
       在 System.Windows.Threading.ExceptionWrapper.TryCatchWhen(Object source, Delegate callback, Object args, Int32 numArgs, Delegate catchHandler)
       在 System.Windows.Threading.Dispatcher.LegacyInvokeImpl(DispatcherPriority priority, TimeSpan timeout, Delegate method, Object args, Int32 numArgs)
       在 MS.Win32.HwndSubclass.SubclassWndProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam)
       在 MS.Win32.UnsafeNativeMethods.DispatchMessage(MSG& msg)
       在 System.Windows.Threading.Dispatcher.PushFrameImpl(DispatcherFrame frame)
       在 System.Windows.Threading.Dispatcher.PushFrame(DispatcherFrame frame)
       在 System.Windows.Application.RunDispatcher(Object ignore)
       在 System.Windows.Application.RunInternal(Window window)
       在 System.Windows.Application.Run(Window window)
       在 System.Windows.Application.Run()
       在 IDCardReaderCrash.App.Main() 位置 c:\Users\cccod\Documents\Visual Studio 2013\Projects\IDCardReaderCrash\IDCardReaderCrash\obj\Debug\App.g.cs:行号 0
       在 System.AppDomain._nExecuteAssembly(RuntimeAssembly assembly, String[] args)
       在 System.AppDomain.ExecuteAssembly(String assemblyFile, Evidence assemblySecurity, String[] args)
       在 Microsoft.VisualStudio.HostingProcess.HostProc.RunUsersAssembly()
       在 System.Threading.ThreadHelper.ThreadStart_Context(Object state)
       在 System.Threading.ExecutionContext.RunInternal(ExecutionContext executionContext, ContextCallback callback, Object state, Boolean preserveSyncCtx)
       在 System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state, Boolean preserveSyncCtx)
       在 System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state)
       在 System.Threading.ThreadHelper.ThreadStart()
  InnerException: 
</code></pre>
<br />
###异常类型2：
<pre><code>
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
</code></pre>
