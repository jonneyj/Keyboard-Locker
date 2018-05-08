Option Strict On
Imports System.Runtime.InteropServices
Imports System
Imports System.IO
Imports System.Text
Public Class Keyboard

    ' Profiles
    Dim MoveBlocked As Boolean
    Dim AllBlocked As Boolean
    Dim ProfileMenu As Boolean
    Dim NumPad As Boolean
    Dim MappingMode As Boolean
    Dim Profile1Toggle As Boolean
    Dim Profile2Toggle As Boolean
    Dim Profile3Toggle As Boolean
    Dim Profile4Toggle As Boolean
    Dim Profile5Toggle As Boolean
    Dim Profile6Toggle As Boolean
    Dim Modval As Boolean
    Dim Keybind As Boolean

    ' F keys
    Dim F1Blocked As Boolean
    Dim F2Blocked As Boolean
    Dim F3Blocked As Boolean
    Dim F4Blocked As Boolean
    Dim F5Blocked As Boolean
    Dim F6Blocked As Boolean
    Dim F7Blocked As Boolean
    Dim F8Blocked As Boolean
    Dim F9Blocked As Boolean
    Dim F10Blocked As Boolean
    Dim F11Blocked As Boolean
    Dim F12Blocked As Boolean

    ' Number Keys
    Dim Num1Blocked As Boolean
    Dim Num2Blocked As Boolean
    Dim Num3Blocked As Boolean
    Dim Num4Blocked As Boolean
    Dim Num5Blocked As Boolean
    Dim Num6Blocked As Boolean
    Dim Num7Blocked As Boolean
    Dim Num8Blocked As Boolean
    Dim Num9Blocked As Boolean
    Dim Num0Blocked As Boolean

    'Number Pad Keys
    Dim ButtonNumPad0Blocked As Boolean
    Dim ButtonNumPad1Blocked As Boolean
    Dim ButtonNumPad2Blocked As Boolean
    Dim ButtonNumPad3Blocked As Boolean
    Dim ButtonNumPad4Blocked As Boolean
    Dim ButtonNumPad5Blocked As Boolean
    Dim ButtonNumPad6Blocked As Boolean
    Dim ButtonNumPad7Blocked As Boolean
    Dim ButtonNumPad8Blocked As Boolean
    Dim ButtonNumPad9Blocked As Boolean
    Dim ButtonNumPadEnterBlocked As Boolean
    Dim ButtonNumPadAddBlocked As Boolean
    Dim ButtonNumPadMultiplyBlocked As Boolean
    Dim ButtonNumPadDivideBlocked As Boolean
    Dim ButtonNumPadNumLockBlocked As Boolean
    Dim ButtonNumPadDecimalBlocked As Boolean
    Dim ButtonNumPadSubtractBlocked As Boolean

    ' Letters Keys
    Dim ButtonABlocked As Boolean
    Dim ButtonBBlocked As Boolean
    Dim ButtonCBlocked As Boolean
    Dim ButtonDBlocked As Boolean
    Dim ButtonEBlocked As Boolean
    Dim ButtonFBlocked As Boolean
    Dim ButtonGBlocked As Boolean
    Dim ButtonHBlocked As Boolean
    Dim ButtonIBlocked As Boolean
    Dim ButtonJBlocked As Boolean
    Dim ButtonKBlocked As Boolean
    Dim ButtonLBlocked As Boolean
    Dim ButtonMBlocked As Boolean
    Dim ButtonNBlocked As Boolean
    Dim ButtonOBlocked As Boolean
    Dim ButtonPBlocked As Boolean
    Dim ButtonQBlocked As Boolean
    Dim ButtonRBlocked As Boolean
    Dim ButtonSBlocked As Boolean
    Dim ButtonTBlocked As Boolean
    Dim ButtonUBlocked As Boolean
    Dim ButtonVBlocked As Boolean
    Dim ButtonWBlocked As Boolean
    Dim ButtonXBlocked As Boolean
    Dim ButtonYBlocked As Boolean
    Dim ButtonZBlocked As Boolean

    ' System Keys
    Dim EscBlocked As Boolean
    Dim ButtonDelBlocked As Boolean
    Dim ButtonLeftCapsBlocked As Boolean
    Dim ButtonLeftCtrlBlocked As Boolean
    Dim ButtonRightCtrlBlocked As Boolean
    Dim ButtonTabBlocked As Boolean
    Dim ButtonLeftShiftBlocked As Boolean
    Dim ButtonRightShiftBlocked As Boolean
    Dim ButtonWinBlocked As Boolean
    Dim ButtonLeftAltBlocked As Boolean
    Dim ButtonRightAltBlocked As Boolean
    Dim ButtonLeftBlocked As Boolean
    Dim ButtonRightBlocked As Boolean
    Dim ButtonDownBlocked As Boolean
    Dim ButtonUpBlocked As Boolean
    Dim ButtonFnBlocked As Boolean

    ' Other Keys
    Dim ButtonSpaceBlocked As Boolean
    Dim BackquoteBlocked As Boolean
    Dim ButtonFocusToggle As Boolean
    Dim ButtonGraveBlocked As Boolean
    Dim ButtonBackspaceBlocked As Boolean
    Dim ButtonAddBlocked As Boolean
    Dim ButtonHyphenBlocked As Boolean
    Dim ButtonCloseBracketsBlocked As Boolean
    Dim ButtonOpenBracketsBlocked As Boolean
    Dim ButtonBackSlashBlocked As Boolean
    Dim ButtonEnterBlocked As Boolean
    Dim ButtonQuotesBlocked As Boolean
    Dim ButtonSemicolenBlocked As Boolean
    Dim ButtonSlashBlocked As Boolean
    Dim ButtonPeriodBlocked As Boolean
    Dim ButtonCommaBlocked As Boolean
    ' This begins the code necesary for the hotkeys
    Class Shortcut
        Inherits NativeWindow
        Implements IDisposable
#Region "Declarations"
        Protected Declare Function UnregisterHotKey Lib "user32.dll" (ByVal Handle As IntPtr, ByVal id As Integer) As Boolean
        Protected Declare Function RegisterHotKey Lib "user32.dll" (ByVal Handle As IntPtr, ByVal id As Integer, ByVal modifier As Integer,
                                                                ByVal vk As Integer) As Boolean
        Event Press(ByVal sender As Object, ByVal e As HotkeyEventArgs)
        Protected EventArgs As HotkeyEventArgs, ID As Integer
        Enum Modifier As Integer
            Alt = 1
            Ctrl = 2
            Shift = 4
        End Enum
        Class HotkeyEventArgs
            Inherits EventArgs
            Property Modifer As Shortcut.Modifier
            Property Key As Keys
        End Class
        Class RegisteredException
            Inherits Exception
            Protected Const s As String = "Shortcut Combination is in use."
            Sub New()
                MyBase.New(s)
            End Sub
        End Class
#End Region
#Region "IDisposable"
        Private disposed As Boolean
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not disposed Then UnregisterHotKey(Handle, ID)
            disposed = True
        End Sub
        Protected Overrides Sub Finalize()
            Dispose(False)
            MyBase.Finalize()
        End Sub
        Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region
        <DebuggerStepperBoundary()>
        Sub New(ByVal modifier As Modifier, ByVal key As Keys)
            CreateHandle(New CreateParams)
            ID = GetHashCode()
            EventArgs = New HotkeyEventArgs With {.Key = key, .Modifer = modifier}
            If Not RegisterHotKey(Handle, ID, modifier, key) Then Throw New RegisteredException
        End Sub
        Shared Function Create(ByVal modifier As Modifier, ByVal key As Keys) As Shortcut
            Return New Shortcut(modifier, key)
        End Function
        Protected Overrides Sub WndProc(ByRef m As Message)
            Select Case m.Msg
                Case 786
                    RaiseEvent Press(Me, EventArgs)
                Case Else
                    MyBase.WndProc(m)
            End Select
        End Sub
    End Class
    'Functions to Lock/Unlock all of the Keys on the OSK
    Function LockAll() As Object
        ProfileLabel.Text = "Locked"
        ProfileLabel.ForeColor = Color.Orange
        F1Blocked = True
        F2Blocked = True
        F3Blocked = True
        F4Blocked = True
        F5Blocked = True
        F6Blocked = True
        F7Blocked = True
        F8Blocked = True
        F9Blocked = True
        F10Blocked = True
        F11Blocked = True
        F12Blocked = True
        Num1Blocked = True
        Num2Blocked = True
        Num3Blocked = True
        Num4Blocked = True
        Num5Blocked = True
        Num6Blocked = True
        Num7Blocked = True
        Num8Blocked = True
        Num9Blocked = True
        Num0Blocked = True
        ButtonABlocked = True
        ButtonBBlocked = True
        ButtonCBlocked = True
        ButtonDBlocked = True
        ButtonEBlocked = True
        ButtonFBlocked = True
        ButtonGBlocked = True
        ButtonHBlocked = True
        ButtonIBlocked = True
        ButtonJBlocked = True
        ButtonKBlocked = True
        ButtonLBlocked = True
        ButtonMBlocked = True
        ButtonNBlocked = True
        ButtonOBlocked = True
        ButtonPBlocked = True
        ButtonQBlocked = True
        ButtonRBlocked = True
        ButtonSBlocked = True
        ButtonTBlocked = True
        ButtonUBlocked = True
        ButtonVBlocked = True
        ButtonWBlocked = True
        ButtonXBlocked = True
        ButtonYBlocked = True
        ButtonZBlocked = True
        EscBlocked = True
        ButtonDelBlocked = True
        ButtonLeftCapsBlocked = True
        ButtonLeftCtrlBlocked = True
        ButtonRightCtrlBlocked = True
        ButtonTabBlocked = True
        ButtonLeftShiftBlocked = True
        ButtonRightShiftBlocked = True
        ButtonWinBlocked = True
        ButtonLeftAltBlocked = True
        ButtonRightAltBlocked = True
        ButtonLeftBlocked = True
        ButtonRightBlocked = True
        ButtonDownBlocked = True
        ButtonUpBlocked = True
        ButtonFnBlocked = True
        ButtonSpaceBlocked = True
        BackquoteBlocked = True
        ButtonGraveBlocked = True
        ButtonBackspaceBlocked = True
        ButtonAddBlocked = True
        ButtonHyphenBlocked = True
        ButtonCloseBracketsBlocked = True
        ButtonOpenBracketsBlocked = True
        ButtonBackSlashBlocked = True
        ButtonEnterBlocked = True
        ButtonQuotesBlocked = True
        ButtonSemicolenBlocked = True
        ButtonSlashBlocked = True
        ButtonPeriodBlocked = True
        ButtonCommaBlocked = True
        ButtonNumPad0Blocked = True
        ButtonNumPad1Blocked = True
        ButtonNumPad2Blocked = True
        ButtonNumPad3Blocked = True
        ButtonNumPad4Blocked = True
        ButtonNumPad5Blocked = True
        ButtonNumPad6Blocked = True
        ButtonNumPad7Blocked = True
        ButtonNumPad8Blocked = True
        ButtonNumPad9Blocked = True
        ButtonNumPadAddBlocked = True
        ButtonNumPadDecimalBlocked = True
        ButtonNumPadDivideBlocked = True
        ButtonNumPadEnterBlocked = True
        ButtonNumPadMultiplyBlocked = True
        ButtonNumPadNumLockBlocked = True
        ButtonNumPadSubtractBlocked = True


        ButtonF1.Cursor = Cursors.No
        ButtonF2.Cursor = Cursors.No
        ButtonF3.Cursor = Cursors.No
        ButtonF4.Cursor = Cursors.No
        ButtonF5.Cursor = Cursors.No
        ButtonF6.Cursor = Cursors.No
        ButtonF7.Cursor = Cursors.No
        ButtonF8.Cursor = Cursors.No
        ButtonF9.Cursor = Cursors.No
        ButtonF10.Cursor = Cursors.No
        ButtonF11.Cursor = Cursors.No
        ButtonF12.Cursor = Cursors.No
        ButtonNum1.Cursor = Cursors.No
        ButtonNum2.Cursor = Cursors.No
        ButtonNum3.Cursor = Cursors.No
        ButtonNum4.Cursor = Cursors.No
        ButtonNum5.Cursor = Cursors.No
        ButtonNum6.Cursor = Cursors.No
        ButtonNum7.Cursor = Cursors.No
        ButtonNum8.Cursor = Cursors.No
        ButtonNum9.Cursor = Cursors.No
        ButtonNum0.Cursor = Cursors.No
        ButtonA.Cursor = Cursors.No
        ButtonB.Cursor = Cursors.No
        ButtonC.Cursor = Cursors.No
        ButtonD.Cursor = Cursors.No
        ButtonE.Cursor = Cursors.No
        ButtonF.Cursor = Cursors.No
        ButtonG.Cursor = Cursors.No
        ButtonH.Cursor = Cursors.No
        ButtonI.Cursor = Cursors.No
        ButtonJ.Cursor = Cursors.No
        ButtonK.Cursor = Cursors.No
        ButtonL.Cursor = Cursors.No
        ButtonM.Cursor = Cursors.No
        ButtonN.Cursor = Cursors.No
        ButtonO.Cursor = Cursors.No
        ButtonP.Cursor = Cursors.No
        ButtonQ.Cursor = Cursors.No
        ButtonR.Cursor = Cursors.No
        ButtonS.Cursor = Cursors.No
        ButtonT.Cursor = Cursors.No
        ButtonU.Cursor = Cursors.No
        ButtonV.Cursor = Cursors.No
        ButtonW.Cursor = Cursors.No
        ButtonX.Cursor = Cursors.No
        ButtonY.Cursor = Cursors.No
        ButtonZ.Cursor = Cursors.No
        buttonEsc.Cursor = Cursors.No
        ButtonDel.Cursor = Cursors.No
        ButtonLeftCaps.Cursor = Cursors.No
        ButtonLeftCtrl.Cursor = Cursors.No
        ButtonRightCtrl.Cursor = Cursors.No
        ButtonTab.Cursor = Cursors.No
        ButtonLeftShift.Cursor = Cursors.No
        ButtonRightShift.Cursor = Cursors.No
        ButtonWin.Cursor = Cursors.No
        ButtonLeftAlt.Cursor = Cursors.No
        ButtonRightAlt.Cursor = Cursors.No
        ButtonLeft.Cursor = Cursors.No
        ButtonRight.Cursor = Cursors.No
        ButtonDown.Cursor = Cursors.No
        ButtonUp.Cursor = Cursors.No
        ButtonFn.Cursor = Cursors.No
        ButtonSpace.Cursor = Cursors.No
        ButtonGrave.Cursor = Cursors.No
        ButtonBackSpace.Cursor = Cursors.No
        ButtonAdd.Cursor = Cursors.No
        ButtonHyphen.Cursor = Cursors.No
        ButtonCloseBrackets.Cursor = Cursors.No
        ButtonOpenBrackets.Cursor = Cursors.No
        ButtonBackSlash.Cursor = Cursors.No
        ButtonEnter.Cursor = Cursors.No
        ButtonQuotes.Cursor = Cursors.No
        ButtonSemicolen.Cursor = Cursors.No
        ButtonSlash.Cursor = Cursors.No
        ButtonPeriod.Cursor = Cursors.No
        ButtonComma.Cursor = Cursors.No
        NumPad0.Cursor = Cursors.No
        NumPad1.Cursor = Cursors.No
        NumPad2.Cursor = Cursors.No
        NumPad3.Cursor = Cursors.No
        NumPad4.Cursor = Cursors.No
        NumPad5.Cursor = Cursors.No
        NumPad6.Cursor = Cursors.No
        NumPad7.Cursor = Cursors.No
        NumPad8.Cursor = Cursors.No
        NumPad9.Cursor = Cursors.No
        NumPadAdd.Cursor = Cursors.No
        NumPadSubtract.Cursor = Cursors.No
        NumPadDecimal.Cursor = Cursors.No
        NumPadDivide.Cursor = Cursors.No
        NumPadEnter.Cursor = Cursors.No
        NumPadMultiply.Cursor = Cursors.No
        NumPadNumLock.Cursor = Cursors.No
        NumPadDecimal.Cursor = Cursors.No

        ButtonF1.BackColor = Color.Orange
        ButtonF2.BackColor = Color.Orange
        ButtonF3.BackColor = Color.Orange
        ButtonF4.BackColor = Color.Orange
        ButtonF5.BackColor = Color.Orange
        ButtonF6.BackColor = Color.Orange
        ButtonF7.BackColor = Color.Orange
        ButtonF8.BackColor = Color.Orange
        ButtonF9.BackColor = Color.Orange
        ButtonF10.BackColor = Color.Orange
        ButtonF11.BackColor = Color.Orange
        ButtonF12.BackColor = Color.Orange
        ButtonNum1.BackColor = Color.Orange
        ButtonNum2.BackColor = Color.Orange
        ButtonNum3.BackColor = Color.Orange
        ButtonNum4.BackColor = Color.Orange
        ButtonNum5.BackColor = Color.Orange
        ButtonNum6.BackColor = Color.Orange
        ButtonNum7.BackColor = Color.Orange
        ButtonNum8.BackColor = Color.Orange
        ButtonNum9.BackColor = Color.Orange
        ButtonNum0.BackColor = Color.Orange
        ButtonA.BackColor = Color.Orange
        ButtonB.BackColor = Color.Orange
        ButtonC.BackColor = Color.Orange
        ButtonD.BackColor = Color.Orange
        ButtonE.BackColor = Color.Orange
        ButtonF.BackColor = Color.Orange
        ButtonG.BackColor = Color.Orange
        ButtonH.BackColor = Color.Orange
        ButtonI.BackColor = Color.Orange
        ButtonJ.BackColor = Color.Orange
        ButtonK.BackColor = Color.Orange
        ButtonL.BackColor = Color.Orange
        ButtonM.BackColor = Color.Orange
        ButtonN.BackColor = Color.Orange
        ButtonO.BackColor = Color.Orange
        ButtonP.BackColor = Color.Orange
        ButtonQ.BackColor = Color.Orange
        ButtonR.BackColor = Color.Orange
        ButtonS.BackColor = Color.Orange
        ButtonT.BackColor = Color.Orange
        ButtonU.BackColor = Color.Orange
        ButtonV.BackColor = Color.Orange
        ButtonW.BackColor = Color.Orange
        ButtonX.BackColor = Color.Orange
        ButtonY.BackColor = Color.Orange
        ButtonZ.BackColor = Color.Orange
        buttonEsc.BackColor = Color.Orange
        ButtonDel.BackColor = Color.Orange
        ButtonLeftCaps.BackColor = Color.Orange
        ButtonLeftCtrl.BackColor = Color.Orange
        ButtonRightCtrl.BackColor = Color.Orange
        ButtonTab.BackColor = Color.Orange
        ButtonLeftShift.BackColor = Color.Orange
        ButtonRightShift.BackColor = Color.Orange
        ButtonWin.BackColor = Color.Orange
        ButtonLeftAlt.BackColor = Color.Orange
        ButtonRightAlt.BackColor = Color.Orange
        ButtonLeft.BackColor = Color.Orange
        ButtonRight.BackColor = Color.Orange
        ButtonDown.BackColor = Color.Orange
        ButtonUp.BackColor = Color.Orange
        ButtonFn.BackColor = Color.Orange
        ButtonSpace.BackColor = Color.Orange
        ButtonGrave.BackColor = Color.Orange
        ButtonBackSpace.BackColor = Color.Orange
        ButtonAdd.BackColor = Color.Orange
        ButtonHyphen.BackColor = Color.Orange
        ButtonCloseBrackets.BackColor = Color.Orange
        ButtonOpenBrackets.BackColor = Color.Orange
        ButtonBackSlash.BackColor = Color.Orange
        ButtonEnter.BackColor = Color.Orange
        ButtonQuotes.BackColor = Color.Orange
        ButtonSemicolen.BackColor = Color.Orange
        ButtonSlash.BackColor = Color.Orange
        ButtonPeriod.BackColor = Color.Orange
        ButtonComma.BackColor = Color.Orange
        NumPad0.BackColor = Color.Orange
        NumPad1.BackColor = Color.Orange
        NumPad2.BackColor = Color.Orange
        NumPad3.BackColor = Color.Orange
        NumPad4.BackColor = Color.Orange
        NumPad5.BackColor = Color.Orange
        NumPad6.BackColor = Color.Orange
        NumPad7.BackColor = Color.Orange
        NumPad8.BackColor = Color.Orange
        NumPad9.BackColor = Color.Orange
        NumPadAdd.BackColor = Color.Orange
        NumPadSubtract.BackColor = Color.Orange
        NumPadDecimal.BackColor = Color.Orange
        NumPadDivide.BackColor = Color.Orange
        NumPadEnter.BackColor = Color.Orange
        NumPadMultiply.BackColor = Color.Orange
        NumPadNumLock.BackColor = Color.Orange
        NumPadDecimal.BackColor = Color.Orange

        ButtonF1.ForeColor = Color.Black
        ButtonF2.ForeColor = Color.Black
        ButtonF3.ForeColor = Color.Black
        ButtonF4.ForeColor = Color.Black
        ButtonF5.ForeColor = Color.Black
        ButtonF6.ForeColor = Color.Black
        ButtonF7.ForeColor = Color.Black
        ButtonF8.ForeColor = Color.Black
        ButtonF9.ForeColor = Color.Black
        ButtonF10.ForeColor = Color.Black
        ButtonF11.ForeColor = Color.Black
        ButtonF12.ForeColor = Color.Black
        ButtonNum1.ForeColor = Color.Black
        ButtonNum2.ForeColor = Color.Black
        ButtonNum3.ForeColor = Color.Black
        ButtonNum4.ForeColor = Color.Black
        ButtonNum5.ForeColor = Color.Black
        ButtonNum6.ForeColor = Color.Black
        ButtonNum7.ForeColor = Color.Black
        ButtonNum8.ForeColor = Color.Black
        ButtonNum9.ForeColor = Color.Black
        ButtonNum0.ForeColor = Color.Black
        ButtonA.ForeColor = Color.Black
        ButtonB.ForeColor = Color.Black
        ButtonC.ForeColor = Color.Black
        ButtonD.ForeColor = Color.Black
        ButtonE.ForeColor = Color.Black
        ButtonF.ForeColor = Color.Black
        ButtonG.ForeColor = Color.Black
        ButtonH.ForeColor = Color.Black
        ButtonI.ForeColor = Color.Black
        ButtonJ.ForeColor = Color.Black
        ButtonK.ForeColor = Color.Black
        ButtonL.ForeColor = Color.Black
        ButtonM.ForeColor = Color.Black
        ButtonN.ForeColor = Color.Black
        ButtonO.ForeColor = Color.Black
        ButtonP.ForeColor = Color.Black
        ButtonQ.ForeColor = Color.Black
        ButtonR.ForeColor = Color.Black
        ButtonS.ForeColor = Color.Black
        ButtonT.ForeColor = Color.Black
        ButtonU.ForeColor = Color.Black
        ButtonV.ForeColor = Color.Black
        ButtonW.ForeColor = Color.Black
        ButtonX.ForeColor = Color.Black
        ButtonY.ForeColor = Color.Black
        ButtonZ.ForeColor = Color.Black
        buttonEsc.ForeColor = Color.Black
        ButtonDel.ForeColor = Color.Black
        ButtonLeftCaps.ForeColor = Color.Black
        ButtonLeftCtrl.ForeColor = Color.Black
        ButtonRightCtrl.ForeColor = Color.Black
        ButtonTab.ForeColor = Color.Black
        ButtonLeftShift.ForeColor = Color.Black
        ButtonRightShift.ForeColor = Color.Black
        ButtonWin.ForeColor = Color.Black
        ButtonLeftAlt.ForeColor = Color.Black
        ButtonRightAlt.ForeColor = Color.Black
        ButtonLeft.ForeColor = Color.Black
        ButtonRight.ForeColor = Color.Black
        ButtonDown.ForeColor = Color.Black
        ButtonUp.ForeColor = Color.Black
        ButtonFn.ForeColor = Color.Black
        ButtonSpace.ForeColor = Color.Black
        ButtonGrave.ForeColor = Color.Black
        ButtonBackSpace.ForeColor = Color.Black
        ButtonAdd.ForeColor = Color.Black
        ButtonHyphen.ForeColor = Color.Black
        ButtonCloseBrackets.ForeColor = Color.Black
        ButtonOpenBrackets.ForeColor = Color.Black
        ButtonBackSlash.ForeColor = Color.Black
        ButtonEnter.ForeColor = Color.Black
        ButtonQuotes.ForeColor = Color.Black
        ButtonSemicolen.ForeColor = Color.Black
        ButtonSlash.ForeColor = Color.Black
        ButtonPeriod.ForeColor = Color.Black
        ButtonComma.ForeColor = Color.Black
        NumPad0.ForeColor = Color.Black
        NumPad1.ForeColor = Color.Black
        NumPad2.ForeColor = Color.Black
        NumPad3.ForeColor = Color.Black
        NumPad4.ForeColor = Color.Black
        NumPad5.ForeColor = Color.Black
        NumPad6.ForeColor = Color.Black
        NumPad7.ForeColor = Color.Black
        NumPad8.ForeColor = Color.Black
        NumPad9.ForeColor = Color.Black
        NumPadAdd.ForeColor = Color.Black
        NumPadSubtract.ForeColor = Color.Black
        NumPadDecimal.ForeColor = Color.Black
        NumPadDivide.ForeColor = Color.Black
        NumPadEnter.ForeColor = Color.Black
        NumPadMultiply.ForeColor = Color.Black
        NumPadNumLock.ForeColor = Color.Black
        NumPadDecimal.ForeColor = Color.Black
    End Function
    Function UnlockAll() As Object
        ProfileLabel.Text = "Unlocked"
        F1Blocked = False
        F2Blocked = False
        F3Blocked = False
        F4Blocked = False
        F5Blocked = False
        F6Blocked = False
        F7Blocked = False
        F8Blocked = False
        F9Blocked = False
        F10Blocked = False
        F11Blocked = False
        F12Blocked = False
        Num1Blocked = False
        Num2Blocked = False
        Num3Blocked = False
        Num4Blocked = False
        Num5Blocked = False
        Num6Blocked = False
        Num7Blocked = False
        Num8Blocked = False
        Num9Blocked = False
        Num0Blocked = False
        ButtonABlocked = False
        ButtonBBlocked = False
        ButtonCBlocked = False
        ButtonDBlocked = False
        ButtonEBlocked = False
        ButtonFBlocked = False
        ButtonGBlocked = False
        ButtonHBlocked = False
        ButtonIBlocked = False
        ButtonJBlocked = False
        ButtonKBlocked = False
        ButtonLBlocked = False
        ButtonMBlocked = False
        ButtonNBlocked = False
        ButtonOBlocked = False
        ButtonPBlocked = False
        ButtonQBlocked = False
        ButtonRBlocked = False
        ButtonSBlocked = False
        ButtonTBlocked = False
        ButtonUBlocked = False
        ButtonVBlocked = False
        ButtonWBlocked = False
        ButtonXBlocked = False
        ButtonYBlocked = False
        ButtonZBlocked = False
        EscBlocked = False
        ButtonDelBlocked = False
        ButtonLeftCapsBlocked = False
        ButtonLeftCtrlBlocked = False
        ButtonRightCtrlBlocked = False
        ButtonTabBlocked = False
        ButtonLeftShiftBlocked = False
        ButtonRightShiftBlocked = False
        ButtonWinBlocked = False
        ButtonLeftAltBlocked = False
        ButtonRightAltBlocked = False
        ButtonLeftBlocked = False
        ButtonRightBlocked = False
        ButtonDownBlocked = False
        ButtonUpBlocked = False
        ButtonFnBlocked = False
        ButtonSpaceBlocked = False
        BackquoteBlocked = False
        ButtonGraveBlocked = False
        ButtonBackspaceBlocked = False
        ButtonAddBlocked = False
        ButtonHyphenBlocked = False
        ButtonCloseBracketsBlocked = False
        ButtonOpenBracketsBlocked = False
        ButtonBackSlashBlocked = False
        ButtonEnterBlocked = False
        ButtonQuotesBlocked = False
        ButtonSemicolenBlocked = False
        ButtonSlashBlocked = False
        ButtonPeriodBlocked = False
        ButtonCommaBlocked = False
        ButtonNumPad0Blocked = False
        ButtonNumPad1Blocked = False
        ButtonNumPad2Blocked = False
        ButtonNumPad3Blocked = False
        ButtonNumPad4Blocked = False
        ButtonNumPad5Blocked = False
        ButtonNumPad6Blocked = False
        ButtonNumPad7Blocked = False
        ButtonNumPad8Blocked = False
        ButtonNumPad9Blocked = False
        ButtonNumPadAddBlocked = False
        ButtonNumPadDecimalBlocked = False
        ButtonNumPadDivideBlocked = False
        ButtonNumPadEnterBlocked = False
        ButtonNumPadMultiplyBlocked = False
        ButtonNumPadNumLockBlocked = False
        ButtonNumPadSubtractBlocked = False


        ButtonF1.Cursor = Cursors.Default
        ButtonF2.Cursor = Cursors.Default
        ButtonF3.Cursor = Cursors.Default
        ButtonF4.Cursor = Cursors.Default
        ButtonF5.Cursor = Cursors.Default
        ButtonF6.Cursor = Cursors.Default
        ButtonF7.Cursor = Cursors.Default
        ButtonF8.Cursor = Cursors.Default
        ButtonF9.Cursor = Cursors.Default
        ButtonF10.Cursor = Cursors.Default
        ButtonF11.Cursor = Cursors.Default
        ButtonF12.Cursor = Cursors.Default
        ButtonNum1.Cursor = Cursors.Default
        ButtonNum2.Cursor = Cursors.Default
        ButtonNum3.Cursor = Cursors.Default
        ButtonNum4.Cursor = Cursors.Default
        ButtonNum5.Cursor = Cursors.Default
        ButtonNum6.Cursor = Cursors.Default
        ButtonNum7.Cursor = Cursors.Default
        ButtonNum8.Cursor = Cursors.Default
        ButtonNum9.Cursor = Cursors.Default
        ButtonNum0.Cursor = Cursors.Default
        ButtonA.Cursor = Cursors.Default
        ButtonB.Cursor = Cursors.Default
        ButtonC.Cursor = Cursors.Default
        ButtonD.Cursor = Cursors.Default
        ButtonE.Cursor = Cursors.Default
        ButtonF.Cursor = Cursors.Default
        ButtonG.Cursor = Cursors.Default
        ButtonH.Cursor = Cursors.Default
        ButtonI.Cursor = Cursors.Default
        ButtonJ.Cursor = Cursors.Default
        ButtonK.Cursor = Cursors.Default
        ButtonL.Cursor = Cursors.Default
        ButtonM.Cursor = Cursors.Default
        ButtonN.Cursor = Cursors.Default
        ButtonO.Cursor = Cursors.Default
        ButtonP.Cursor = Cursors.Default
        ButtonQ.Cursor = Cursors.Default
        ButtonR.Cursor = Cursors.Default
        ButtonS.Cursor = Cursors.Default
        ButtonT.Cursor = Cursors.Default
        ButtonU.Cursor = Cursors.Default
        ButtonV.Cursor = Cursors.Default
        ButtonW.Cursor = Cursors.Default
        ButtonX.Cursor = Cursors.Default
        ButtonY.Cursor = Cursors.Default
        ButtonZ.Cursor = Cursors.Default
        buttonEsc.Cursor = Cursors.Default
        ButtonDel.Cursor = Cursors.Default
        ButtonLeftCaps.Cursor = Cursors.Default
        ButtonLeftCtrl.Cursor = Cursors.Default
        ButtonRightCtrl.Cursor = Cursors.Default
        ButtonTab.Cursor = Cursors.Default
        ButtonLeftShift.Cursor = Cursors.Default
        ButtonRightShift.Cursor = Cursors.Default
        ButtonWin.Cursor = Cursors.Default
        ButtonLeftAlt.Cursor = Cursors.Default
        ButtonRightAlt.Cursor = Cursors.Default
        ButtonLeft.Cursor = Cursors.Default
        ButtonRight.Cursor = Cursors.Default
        ButtonDown.Cursor = Cursors.Default
        ButtonUp.Cursor = Cursors.Default
        ButtonFn.Cursor = Cursors.Default
        ButtonSpace.Cursor = Cursors.Default
        ButtonGrave.Cursor = Cursors.Default
        ButtonBackSpace.Cursor = Cursors.Default
        ButtonAdd.Cursor = Cursors.Default
        ButtonHyphen.Cursor = Cursors.Default
        ButtonCloseBrackets.Cursor = Cursors.Default
        ButtonOpenBrackets.Cursor = Cursors.Default
        ButtonBackSlash.Cursor = Cursors.Default
        ButtonEnter.Cursor = Cursors.Default
        ButtonQuotes.Cursor = Cursors.Default
        ButtonSemicolen.Cursor = Cursors.Default
        ButtonSlash.Cursor = Cursors.Default
        ButtonPeriod.Cursor = Cursors.Default
        ButtonComma.Cursor = Cursors.Default
        NumPad0.Cursor = Cursors.Default
        NumPad1.Cursor = Cursors.Default
        NumPad2.Cursor = Cursors.Default
        NumPad3.Cursor = Cursors.Default
        NumPad4.Cursor = Cursors.Default
        NumPad5.Cursor = Cursors.Default
        NumPad6.Cursor = Cursors.Default
        NumPad7.Cursor = Cursors.Default
        NumPad8.Cursor = Cursors.Default
        NumPad9.Cursor = Cursors.Default
        NumPadAdd.Cursor = Cursors.Default
        NumPadSubtract.Cursor = Cursors.Default
        NumPadDecimal.Cursor = Cursors.Default
        NumPadDivide.Cursor = Cursors.Default
        NumPadEnter.Cursor = Cursors.Default
        NumPadMultiply.Cursor = Cursors.Default
        NumPadNumLock.Cursor = Cursors.Default
        NumPadDecimal.Cursor = Cursors.Default

        ButtonF1.BackColor = Color.FromArgb(51, 51, 51)
        ButtonF2.BackColor = Color.FromArgb(51, 51, 51)
        ButtonF3.BackColor = Color.FromArgb(51, 51, 51)
        ButtonF4.BackColor = Color.FromArgb(51, 51, 51)
        ButtonF5.BackColor = Color.FromArgb(51, 51, 51)
        ButtonF6.BackColor = Color.FromArgb(51, 51, 51)
        ButtonF7.BackColor = Color.FromArgb(51, 51, 51)
        ButtonF8.BackColor = Color.FromArgb(51, 51, 51)
        ButtonF9.BackColor = Color.FromArgb(51, 51, 51)
        ButtonF10.BackColor = Color.FromArgb(51, 51, 51)
        ButtonF11.BackColor = Color.FromArgb(51, 51, 51)
        ButtonF12.BackColor = Color.FromArgb(51, 51, 51)
        ButtonNum1.BackColor = Color.FromArgb(51, 51, 51)
        ButtonNum2.BackColor = Color.FromArgb(51, 51, 51)
        ButtonNum3.BackColor = Color.FromArgb(51, 51, 51)
        ButtonNum4.BackColor = Color.FromArgb(51, 51, 51)
        ButtonNum5.BackColor = Color.FromArgb(51, 51, 51)
        ButtonNum6.BackColor = Color.FromArgb(51, 51, 51)
        ButtonNum7.BackColor = Color.FromArgb(51, 51, 51)
        ButtonNum8.BackColor = Color.FromArgb(51, 51, 51)
        ButtonNum9.BackColor = Color.FromArgb(51, 51, 51)
        ButtonNum0.BackColor = Color.FromArgb(51, 51, 51)
        ButtonA.BackColor = Color.FromArgb(51, 51, 51)
        ButtonB.BackColor = Color.FromArgb(51, 51, 51)
        ButtonC.BackColor = Color.FromArgb(51, 51, 51)
        ButtonD.BackColor = Color.FromArgb(51, 51, 51)
        ButtonE.BackColor = Color.FromArgb(51, 51, 51)
        ButtonF.BackColor = Color.FromArgb(51, 51, 51)
        ButtonG.BackColor = Color.FromArgb(51, 51, 51)
        ButtonH.BackColor = Color.FromArgb(51, 51, 51)
        ButtonI.BackColor = Color.FromArgb(51, 51, 51)
        ButtonJ.BackColor = Color.FromArgb(51, 51, 51)
        ButtonK.BackColor = Color.FromArgb(51, 51, 51)
        ButtonL.BackColor = Color.FromArgb(51, 51, 51)
        ButtonM.BackColor = Color.FromArgb(51, 51, 51)
        ButtonN.BackColor = Color.FromArgb(51, 51, 51)
        ButtonO.BackColor = Color.FromArgb(51, 51, 51)
        ButtonP.BackColor = Color.FromArgb(51, 51, 51)
        ButtonQ.BackColor = Color.FromArgb(51, 51, 51)
        ButtonR.BackColor = Color.FromArgb(51, 51, 51)
        ButtonS.BackColor = Color.FromArgb(51, 51, 51)
        ButtonT.BackColor = Color.FromArgb(51, 51, 51)
        ButtonU.BackColor = Color.FromArgb(51, 51, 51)
        ButtonV.BackColor = Color.FromArgb(51, 51, 51)
        ButtonW.BackColor = Color.FromArgb(51, 51, 51)
        ButtonX.BackColor = Color.FromArgb(51, 51, 51)
        ButtonY.BackColor = Color.FromArgb(51, 51, 51)
        ButtonZ.BackColor = Color.FromArgb(51, 51, 51)
        buttonEsc.BackColor = Color.FromArgb(51, 51, 51)
        ButtonDel.BackColor = Color.FromArgb(51, 51, 51)
        ButtonLeftCaps.BackColor = Color.FromArgb(51, 51, 51)
        ButtonLeftCtrl.BackColor = Color.FromArgb(51, 51, 51)
        ButtonRightCtrl.BackColor = Color.FromArgb(51, 51, 51)
        ButtonTab.BackColor = Color.FromArgb(51, 51, 51)
        ButtonLeftShift.BackColor = Color.FromArgb(51, 51, 51)
        ButtonRightShift.BackColor = Color.FromArgb(51, 51, 51)
        ButtonWin.BackColor = Color.FromArgb(51, 51, 51)
        ButtonLeftAlt.BackColor = Color.FromArgb(51, 51, 51)
        ButtonRightAlt.BackColor = Color.FromArgb(51, 51, 51)
        ButtonLeft.BackColor = Color.FromArgb(51, 51, 51)
        ButtonRight.BackColor = Color.FromArgb(51, 51, 51)
        ButtonDown.BackColor = Color.FromArgb(51, 51, 51)
        ButtonUp.BackColor = Color.FromArgb(51, 51, 51)
        ButtonFn.BackColor = Color.FromArgb(51, 51, 51)
        ButtonSpace.BackColor = Color.FromArgb(51, 51, 51)
        ButtonGrave.BackColor = Color.FromArgb(51, 51, 51)
        ButtonBackSpace.BackColor = Color.FromArgb(51, 51, 51)
        ButtonAdd.BackColor = Color.FromArgb(51, 51, 51)
        ButtonHyphen.BackColor = Color.FromArgb(51, 51, 51)
        ButtonCloseBrackets.BackColor = Color.FromArgb(51, 51, 51)
        ButtonOpenBrackets.BackColor = Color.FromArgb(51, 51, 51)
        ButtonBackSlash.BackColor = Color.FromArgb(51, 51, 51)
        ButtonEnter.BackColor = Color.FromArgb(51, 51, 51)
        ButtonQuotes.BackColor = Color.FromArgb(51, 51, 51)
        ButtonSemicolen.BackColor = Color.FromArgb(51, 51, 51)
        ButtonSlash.BackColor = Color.FromArgb(51, 51, 51)
        ButtonPeriod.BackColor = Color.FromArgb(51, 51, 51)
        ButtonComma.BackColor = Color.FromArgb(51, 51, 51)
        NumPad0.BackColor = Color.FromArgb(51, 51, 51)
        NumPad1.BackColor = Color.FromArgb(51, 51, 51)
        NumPad2.BackColor = Color.FromArgb(51, 51, 51)
        NumPad3.BackColor = Color.FromArgb(51, 51, 51)
        NumPad4.BackColor = Color.FromArgb(51, 51, 51)
        NumPad5.BackColor = Color.FromArgb(51, 51, 51)
        NumPad6.BackColor = Color.FromArgb(51, 51, 51)
        NumPad7.BackColor = Color.FromArgb(51, 51, 51)
        NumPad8.BackColor = Color.FromArgb(51, 51, 51)
        NumPad9.BackColor = Color.FromArgb(51, 51, 51)
        NumPadAdd.BackColor = Color.FromArgb(51, 51, 51)
        NumPadSubtract.BackColor = Color.FromArgb(51, 51, 51)
        NumPadDecimal.BackColor = Color.FromArgb(51, 51, 51)
        NumPadDivide.BackColor = Color.FromArgb(51, 51, 51)
        NumPadEnter.BackColor = Color.FromArgb(51, 51, 51)
        NumPadMultiply.BackColor = Color.FromArgb(51, 51, 51)
        NumPadNumLock.BackColor = Color.FromArgb(51, 51, 51)
        NumPadDecimal.BackColor = Color.FromArgb(51, 51, 51)

        ButtonF1.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonF2.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonF3.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonF4.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonF5.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonF6.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonF7.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonF8.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonF9.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonF10.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonF11.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonF12.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonNum1.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonNum2.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonNum3.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonNum4.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonNum5.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonNum6.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonNum7.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonNum8.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonNum9.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonNum0.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonA.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonB.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonC.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonD.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonE.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonF.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonG.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonH.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonI.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonJ.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonK.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonL.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonM.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonN.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonO.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonP.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonQ.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonR.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonS.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonT.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonU.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonV.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonW.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonX.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonY.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonZ.ForeColor = Color.FromArgb(221, 221, 221)
        buttonEsc.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonDel.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonLeftCaps.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonLeftCtrl.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonRightCtrl.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonTab.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonLeftShift.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonRightShift.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonWin.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonLeftAlt.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonRightAlt.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonLeft.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonRight.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonDown.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonUp.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonFn.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonSpace.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonGrave.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonBackSpace.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonAdd.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonHyphen.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonCloseBrackets.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonOpenBrackets.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonBackSlash.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonEnter.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonQuotes.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonSemicolen.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonSlash.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonPeriod.ForeColor = Color.FromArgb(221, 221, 221)
        ButtonComma.ForeColor = Color.FromArgb(221, 221, 221)
        NumPad0.ForeColor = Color.FromArgb(221, 221, 221)
        NumPad1.ForeColor = Color.FromArgb(221, 221, 221)
        NumPad2.ForeColor = Color.FromArgb(221, 221, 221)
        NumPad3.ForeColor = Color.FromArgb(221, 221, 221)
        NumPad4.ForeColor = Color.FromArgb(221, 221, 221)
        NumPad5.ForeColor = Color.FromArgb(221, 221, 221)
        NumPad6.ForeColor = Color.FromArgb(221, 221, 221)
        NumPad7.ForeColor = Color.FromArgb(221, 221, 221)
        NumPad8.ForeColor = Color.FromArgb(221, 221, 221)
        NumPad9.ForeColor = Color.FromArgb(221, 221, 221)
        NumPadAdd.ForeColor = Color.FromArgb(221, 221, 221)
        NumPadSubtract.ForeColor = Color.FromArgb(221, 221, 221)
        NumPadDecimal.ForeColor = Color.FromArgb(221, 221, 221)
        NumPadDivide.ForeColor = Color.FromArgb(221, 221, 221)
        NumPadEnter.ForeColor = Color.FromArgb(221, 221, 221)
        NumPadMultiply.ForeColor = Color.FromArgb(221, 221, 221)
        NumPadNumLock.ForeColor = Color.FromArgb(221, 221, 221)
        NumPadDecimal.ForeColor = Color.FromArgb(221, 221, 221)
    End Function
    Public Function U_Press() As Boolean Handles U.Press
        Call Function() UnlockAll()
    End Function
    Private Sub L_Press() Handles L.Press
        Call Function() LockAll()
    End Sub
    Dim WithEvents L As Shortcut
    Dim WithEvents U As Shortcut
    Private Sub Form_Load() Handles MyBase.Load
        L = Shortcut.Create(Shortcut.Modifier.Ctrl, Keys.L)
        U = Shortcut.Create(Shortcut.Modifier.Ctrl, Keys.U)
    End Sub
    'This ends the Hotkey Code

    'This is the Keyboard DLL Hook Struct that is used in the key blocking function
    Private Structure KBDLLHOOKSTRUCT
        Public key As Keys
        Public scanCode As Integer
        Public flags As Integer
        Public time As Integer
        Public extra As IntPtr
    End Structure
    'System level functions to be used for hook and unhook keyboard input
    Private Delegate Function LowLevelKeyboardProc(ByVal nCode As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function SetWindowsHookEx(ByVal id As Integer, ByVal callback As LowLevelKeyboardProc, ByVal hMod As IntPtr, ByVal dwThreadId As UInteger) As IntPtr
    End Function
    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function UnhookWindowsHookEx(ByVal hook As IntPtr) As Boolean
    End Function
    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function CallNextHookEx(ByVal hook As IntPtr, ByVal nCode As Integer, ByVal wp As IntPtr, ByVal lp As IntPtr) As IntPtr
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function GetModuleHandle(ByVal name As String) As IntPtr
    End Function
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function GetAsyncKeyState(ByVal key As Keys) As Short
    End Function
    'Declaring Global objects for the Blocking function
    Private ptrHook As IntPtr
    Private objKeyboardProcess As LowLevelKeyboardProc
    Private objKeyInfo As Object
    Public Property Keycode As Object
    Public ModifierCode As Object
    'Sub for Key blocking
    Public Sub New()
        Try
            Dim objCurrentModule As ProcessModule = Process.GetCurrentProcess().MainModule
            'Get Current Module
            objKeyboardProcess = New LowLevelKeyboardProc(AddressOf CaptureKey)
            'Assign callback function each time keyboard process
            ptrHook = SetWindowsHookEx(13, objKeyboardProcess, GetModuleHandle(objCurrentModule.ModuleName), 0)
            'Setting Hook of Keyboard Process for current module
            ' This call is required by the Windows Form Designer.
            InitializeComponent()
            ' Add any initialization after the InitializeComponent() call.
        Catch ex As Exception
        End Try
    End Sub
    'Function that blocks the keys
    Private Function CaptureKey(ByVal nCode As Integer, ByVal wp As IntPtr, ByVal lp As IntPtr) As IntPtr
        Try
            If nCode >= 0 Then
                Dim objKeyInfo As KBDLLHOOKSTRUCT = DirectCast(Marshal.PtrToStructure(lp, GetType(KBDLLHOOKSTRUCT)), KBDLLHOOKSTRUCT)

                If F1Blocked = True Then
                    If objKeyInfo.key = Keys.F1 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If F2Blocked = True Then
                    If objKeyInfo.key = Keys.F2 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If F3Blocked = True Then
                    If objKeyInfo.key = Keys.F3 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If F4Blocked = True Then
                    If objKeyInfo.key = Keys.F4 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If F5Blocked = True Then
                    If objKeyInfo.key = Keys.F5 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If F6Blocked = True Then
                    If objKeyInfo.key = Keys.F6 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If F7Blocked = True Then
                    If objKeyInfo.key = Keys.F7 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If F8Blocked = True Then
                    If objKeyInfo.key = Keys.F8 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If F9Blocked = True Then
                    If objKeyInfo.key = Keys.F9 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If F10Blocked = True Then
                    If objKeyInfo.key = Keys.F10 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If F11Blocked = True Then
                    If objKeyInfo.key = Keys.F11 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If F12Blocked = True Then
                    If objKeyInfo.key = Keys.F12 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If EscBlocked = True Then
                    If objKeyInfo.key = Keys.Escape Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonGraveBlocked = True Then
                    If objKeyInfo.key = Keys.Oemtilde Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonABlocked = True Then
                    If objKeyInfo.key = Keys.A Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonTabBlocked = True Then
                    If objKeyInfo.key = Keys.Tab Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If Num1Blocked = True Then
                    If objKeyInfo.key = Keys.D1 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If Num2Blocked = True Then
                    If objKeyInfo.key = Keys.D2 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If Num3Blocked = True Then
                    If objKeyInfo.key = Keys.D3 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If Num4Blocked = True Then
                    If objKeyInfo.key = Keys.D4 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If Num5Blocked = True Then
                    If objKeyInfo.key = Keys.D5 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If Num6Blocked = True Then
                    If objKeyInfo.key = Keys.D6 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If Num7Blocked = True Then
                    If objKeyInfo.key = Keys.D7 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If Num8Blocked = True Then
                    If objKeyInfo.key = Keys.D8 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If Num9Blocked = True Then
                    If objKeyInfo.key = Keys.D9 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If Num0Blocked = True Then
                    If objKeyInfo.key = Keys.D0 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonHyphenBlocked = True Then
                    If objKeyInfo.key = Keys.OemMinus Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonAddBlocked = True Then
                    If objKeyInfo.key = Keys.Oemplus Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonBackspaceBlocked = True Then
                    If objKeyInfo.key = Keys.Back Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonQBlocked = True Then
                    If objKeyInfo.key = Keys.Q Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonWBlocked = True Then
                    If objKeyInfo.key = Keys.W Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonEBlocked = True Then
                    If objKeyInfo.key = Keys.E Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonRBlocked = True Then
                    If objKeyInfo.key = Keys.R Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonTBlocked = True Then
                    If objKeyInfo.key = Keys.T Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonYBlocked = True Then
                    If objKeyInfo.key = Keys.Y Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonUBlocked = True Then
                    If objKeyInfo.key = Keys.Control And objKeyInfo.key = Keys.U Then
                        Return CType(0, IntPtr)
                        Call Function() UnlockAll()
                    ElseIf objKeyInfo.key = Keys.U Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonIBlocked = True Then
                    If objKeyInfo.key = Keys.I Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonOBlocked = True Then
                    If objKeyInfo.key = Keys.O Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonPBlocked = True Then
                    If objKeyInfo.key = Keys.P Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonOpenBracketsBlocked = True Then
                    If objKeyInfo.key = Keys.OemOpenBrackets Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonCloseBracketsBlocked = True Then
                    If objKeyInfo.key = Keys.OemCloseBrackets Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonBackSlashBlocked = True Then
                    If objKeyInfo.key = Keys.OemPipe Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonDelBlocked = True Then
                    If objKeyInfo.key = Keys.Delete Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonLeftCapsBlocked = True Then
                    If objKeyInfo.key = Keys.CapsLock Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonSBlocked = True Then
                    If objKeyInfo.key = Keys.S Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonDBlocked = True Then
                    If objKeyInfo.key = Keys.D Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonFBlocked = True Then
                    If objKeyInfo.key = Keys.F Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonGBlocked = True Then
                    If objKeyInfo.key = Keys.G Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonHBlocked = True Then
                    If objKeyInfo.key = Keys.H Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonJBlocked = True Then
                    If objKeyInfo.key = Keys.J Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonKBlocked = True Then
                    If objKeyInfo.key = Keys.K Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonLBlocked = True Then
                    If objKeyInfo.key = Keys.L Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonSemicolenBlocked = True Then
                    If objKeyInfo.key = Keys.OemSemicolon Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonQuotesBlocked = True Then
                    If objKeyInfo.key = Keys.OemQuotes Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonEnterBlocked = True Then
                    If objKeyInfo.key = Keys.Enter Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonLeftShiftBlocked = True Then
                    If objKeyInfo.key = Keys.LShiftKey Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonZBlocked = True Then
                    If objKeyInfo.key = Keys.Z Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonXBlocked = True Then
                    If objKeyInfo.key = Keys.X Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonCBlocked = True Then
                    If objKeyInfo.key = Keys.C Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonVBlocked = True Then
                    If objKeyInfo.key = Keys.V Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonBBlocked = True Then
                    If objKeyInfo.key = Keys.B Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonNBlocked = True Then
                    If objKeyInfo.key = Keys.N Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonMBlocked = True Then
                    If objKeyInfo.key = Keys.M Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonCommaBlocked = True Then
                    If objKeyInfo.key = Keys.Oemcomma Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonPeriodBlocked = True Then
                    If objKeyInfo.key = Keys.OemPeriod Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonSlashBlocked = True Then
                    If objKeyInfo.key = Keys.OemQuestion Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonUpBlocked = True Then
                    If objKeyInfo.key = Keys.Up Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonRightShiftBlocked = True Then
                    If objKeyInfo.key = Keys.RShiftKey Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonLeftCtrlBlocked = True Then
                    If objKeyInfo.key = Keys.ControlKey Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonWinBlocked = True Then
                    If objKeyInfo.key = Keys.LWin Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonWinBlocked = True Then
                    If objKeyInfo.key = Keys.RWin Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonLeftAltBlocked = True Then
                    If objKeyInfo.key = Keys.LMenu Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonRightAltBlocked = True Then
                    If objKeyInfo.key = Keys.RMenu Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonSpaceBlocked = True Then
                    If objKeyInfo.key = Keys.Space Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonRightCtrlBlocked = True Then
                    If objKeyInfo.key = Keys.Control Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonLeftBlocked = True Then
                    If objKeyInfo.key = Keys.Left Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonDownBlocked = True Then
                    If objKeyInfo.key = Keys.Down Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonRightBlocked = True Then
                    If objKeyInfo.key = Keys.Right Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonNumPad0Blocked = True Then
                    If objKeyInfo.key = Keys.NumPad0 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonNumPad1Blocked = True Then
                    If objKeyInfo.key = Keys.NumPad1 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonNumPad2Blocked = True Then
                    If objKeyInfo.key = Keys.NumPad2 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonNumPad3Blocked = True Then
                    If objKeyInfo.key = Keys.NumPad3 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonNumPad4Blocked = True Then
                    If objKeyInfo.key = Keys.NumPad4 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonNumPad5Blocked = True Then
                    If objKeyInfo.key = Keys.NumPad5 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonNumPad6Blocked = True Then
                    If objKeyInfo.key = Keys.NumPad6 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonNumPad7Blocked = True Then
                    If objKeyInfo.key = Keys.NumPad7 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonNumPad8Blocked = True Then
                    If objKeyInfo.key = Keys.NumPad8 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonNumPad9Blocked = True Then
                    If objKeyInfo.key = Keys.NumPad9 Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonNumPadAddBlocked = True Then
                    If objKeyInfo.key = Keys.Add Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonNumPadDecimalBlocked = True Then
                    If objKeyInfo.key = Keys.Decimal Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonNumPadMultiplyBlocked = True Then
                    If objKeyInfo.key = Keys.Multiply Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonNumPadDivideBlocked = True Then
                    If objKeyInfo.key = Keys.Divide Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonNumPadEnterBlocked = True Then
                    If objKeyInfo.key = Keys.Enter Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonNumPadNumLockBlocked = True Then
                    If objKeyInfo.key = Keys.NumLock Then
                        Return CType(1, IntPtr)
                    End If
                End If
                If ButtonNumPadSubtractBlocked Then
                    If objKeyInfo.key = Keys.Subtract Then
                        Return CType(1, IntPtr)
                    End If
                End If
                Return CallNextHookEx(ptrHook, nCode, wp, lp)
            End If
        Catch ex As Exception
        End Try
    End Function
    'Allows mouse cursor and color change on mouse events on buttons.
    Function ButtonTestfor(ByRef buttonVar As Boolean, eventButton As Button, mouseEvent As MouseEventArgs) As Object
        If mouseEvent.Button = MouseButtons.Left Then
            If buttonVar = True Then
                eventButton.Cursor = Cursors.Default
                eventButton.BackColor = Color.FromArgb(51, 51, 51)
                eventButton.ForeColor = Color.FromArgb(221, 221, 221)
                buttonVar = False
            ElseIf buttonVar = False Then
                eventButton.Cursor = Cursors.No
                eventButton.BackColor = Color.Orange
                eventButton.ForeColor = Color.Black
                buttonVar = True
            End If
        End If
    End Function
    Private Sub Keyboard_Form(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ctl As Control
        Dim ctlMDI As MdiClient
        ' Loop through all of the form's controls looking
        ' for the control of type MdiClient.
        For Each ctl In Me.Controls
            Try
                ' Attempt to cast the control to type MdiClient.
                ctlMDI = CType(ctl, MdiClient)
                ' Set the BackColor of the MdiClient control.
                ctlMDI.BackColor = Color.FromArgb(26, 26, 26)
            Catch exc As InvalidCastException
                ' Catch and ignore the error if casting failed.
            End Try
        Next
    End Sub
    'Subs that insitute the ButtonTestFor function 
    'This allows all keys to change cursor and color without as many lines of code
    Private Sub ButtonF1_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonF1.MouseDown
        ButtonTestfor(F1Blocked, ButtonF1, e)
    End Sub
    Private Sub ButtonF2_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonF2.MouseDown
        ButtonTestfor(F2Blocked, ButtonF2, e)
    End Sub
    Private Sub ButtonF3_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonF3.MouseDown
        ButtonTestfor(F3Blocked, ButtonF3, e)
    End Sub
    Private Sub ButtonF4_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonF4.MouseDown
        ButtonTestfor(F4Blocked, ButtonF4, e)
    End Sub
    Private Sub ButtonF5_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonF5.MouseDown
        ButtonTestfor(F5Blocked, ButtonF5, e)
    End Sub
    Private Sub ButtonF6_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonF6.MouseDown
        ButtonTestfor(F6Blocked, ButtonF6, e)
    End Sub
    Private Sub ButtonF7_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonF7.MouseDown
        ButtonTestfor(F7Blocked, ButtonF7, e)
    End Sub
    Private Sub ButtonF8_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonF8.MouseDown
        ButtonTestfor(F8Blocked, ButtonF8, e)
    End Sub
    Private Sub ButtonF9_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonF9.MouseDown
        ButtonTestfor(F9Blocked, ButtonF9, e)
    End Sub
    Private Sub ButtonF10_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonF10.MouseDown
        ButtonTestfor(F10Blocked, ButtonF10, e)
    End Sub
    Private Sub ButtonF11_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonF11.MouseDown
        ButtonTestfor(F11Blocked, ButtonF11, e)
    End Sub
    Private Sub ButtonF12_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonF12.MouseDown
        ButtonTestfor(F12Blocked, ButtonF12, e)
    End Sub
    Private Sub ButtonEsc_MouseDown(sender As Object, e As MouseEventArgs) Handles buttonEsc.MouseDown
        ButtonTestfor(EscBlocked, buttonEsc, e)
    End Sub
    Private Sub ButtonGrave_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonGrave.MouseDown
        ButtonTestfor(ButtonGraveBlocked, ButtonGrave, e)
    End Sub
    Private Sub ButtonA_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonA.MouseDown
        ButtonTestfor(ButtonABlocked, ButtonA, e)
    End Sub
    Private Sub ButtonTab_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonTab.MouseDown
        ButtonTestfor(ButtonTabBlocked, ButtonTab, e)
    End Sub
    Private Sub ButtonNum1_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonNum1.MouseDown
        ButtonTestfor(Num1Blocked, ButtonNum1, e)
    End Sub
    Private Sub ButtonNum2_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonNum2.MouseDown
        ButtonTestfor(Num2Blocked, ButtonNum2, e)
    End Sub
    Private Sub ButtonNum3_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonNum3.MouseDown
        ButtonTestfor(Num3Blocked, ButtonNum3, e)
    End Sub
    Private Sub ButtonNum4_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonNum4.MouseDown
        ButtonTestfor(Num4Blocked, ButtonNum4, e)
    End Sub
    Private Sub ButtonNum5_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonNum5.MouseDown
        ButtonTestfor(Num5Blocked, ButtonNum5, e)
    End Sub
    Private Sub ButtonNum6_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonNum6.MouseDown
        ButtonTestfor(Num6Blocked, ButtonNum6, e)
    End Sub
    Private Sub ButtonNum7_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonNum7.MouseDown
        ButtonTestfor(Num7Blocked, ButtonNum7, e)
    End Sub
    Private Sub ButtonNum8_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonNum8.MouseDown
        ButtonTestfor(Num8Blocked, ButtonNum8, e)
    End Sub
    Private Sub ButtonNum9_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonNum9.MouseDown
        ButtonTestfor(Num9Blocked, ButtonNum9, e)
    End Sub
    Private Sub ButtonNum0_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonNum0.MouseDown
        ButtonTestfor(Num0Blocked, ButtonNum0, e)
    End Sub
    Private Sub ButtonHyphen_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonHyphen.MouseDown
        ButtonTestfor(ButtonHyphenBlocked, ButtonHyphen, e)
    End Sub
    Private Sub ButtonQ_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonQ.MouseDown
        ButtonTestfor(ButtonQBlocked, ButtonQ, e)
    End Sub
    Private Sub ButtonW_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonW.MouseDown
        ButtonTestfor(ButtonWBlocked, ButtonW, e)
    End Sub
    Private Sub ButtonE_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonE.MouseDown
        ButtonTestfor(ButtonEBlocked, ButtonE, e)
    End Sub
    Private Sub ButtonR_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonR.MouseDown
        ButtonTestfor(ButtonRBlocked, ButtonR, e)
    End Sub
    Private Sub ButtonT_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonT.MouseDown
        ButtonTestfor(ButtonTBlocked, ButtonT, e)
    End Sub
    Private Sub ButtonY_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonY.MouseDown
        ButtonTestfor(ButtonYBlocked, ButtonY, e)
    End Sub
    Private Sub ButtonU_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonU.MouseDown
        ButtonTestfor(ButtonUBlocked, ButtonU, e)
    End Sub
    Private Sub ButtonI_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonI.MouseDown
        ButtonTestfor(ButtonIBlocked, ButtonI, e)
    End Sub
    Private Sub ButtonO_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonO.MouseDown
        ButtonTestfor(ButtonOBlocked, ButtonO, e)
    End Sub
    Private Sub ButtonP_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonP.MouseDown
        ButtonTestfor(ButtonPBlocked, ButtonP, e)
    End Sub
    Private Sub ButtonSBlocked_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonS.MouseDown
        ButtonTestfor(ButtonSBlocked, ButtonS, e)
    End Sub
    Private Sub ButtonD_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonD.MouseDown
        ButtonTestfor(ButtonDBlocked, ButtonD, e)
    End Sub
    Private Sub ButtonAdd_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonAdd.MouseDown
        ButtonTestfor(ButtonAddBlocked, ButtonAdd, e)
    End Sub
    Private Sub ButtonBackspace_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonBackSpace.MouseDown
        ButtonTestfor(ButtonBackspaceBlocked, ButtonBackSpace, e)
    End Sub
    Private Sub ButtonOpenBrackets_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonOpenBrackets.MouseDown
        ButtonTestfor(ButtonOpenBracketsBlocked, ButtonOpenBrackets, e)
    End Sub
    Private Sub ButtonCloseBrackets_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonCloseBrackets.MouseDown
        ButtonTestfor(ButtonCloseBracketsBlocked, ButtonCloseBrackets, e)
    End Sub
    Private Sub ButtonBackSlash_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonBackSlash.MouseDown
        ButtonTestfor(ButtonBackSlashBlocked, ButtonBackSlash, e)
    End Sub
    Private Sub ButtonDel_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonDel.MouseDown
        ButtonTestfor(ButtonDelBlocked, ButtonDel, e)
    End Sub
    Private Sub ButtonLeftCaps_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonLeftCaps.MouseDown
        ButtonTestfor(ButtonLeftCapsBlocked, ButtonLeftCaps, e)
    End Sub
    Private Sub ButtonF_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonF.MouseDown
        ButtonTestfor(ButtonFBlocked, ButtonF, e)
    End Sub
    Private Sub ButtonG_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonG.MouseDown
        ButtonTestfor(ButtonGBlocked, ButtonG, e)
    End Sub
    Private Sub ButtonH_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonH.MouseDown
        ButtonTestfor(ButtonHBlocked, ButtonH, e)
    End Sub
    Private Sub ButtonJ_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonJ.MouseDown
        ButtonTestfor(ButtonJBlocked, ButtonJ, e)
    End Sub
    Private Sub ButtonK_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonK.MouseDown
        ButtonTestfor(ButtonKBlocked, ButtonK, e)
    End Sub
    Private Sub ButtonL_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonL.MouseDown
        ButtonTestfor(ButtonLBlocked, ButtonL, e)
    End Sub
    Private Sub ButtonSemicolen_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonSemicolen.MouseDown
        ButtonTestfor(ButtonSemicolenBlocked, ButtonSemicolen, e)
    End Sub
    Private Sub ButtonQuotes_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonQuotes.MouseDown
        ButtonTestfor(ButtonQuotesBlocked, ButtonQuotes, e)
    End Sub
    Private Sub ButtonEnter_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonEnter.MouseDown
        ButtonTestfor(ButtonEnterBlocked, ButtonEnter, e)
    End Sub
    Private Sub ButtonLeftShift_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonLeftShift.MouseDown
        ButtonTestfor(ButtonLeftShiftBlocked, ButtonLeftShift, e)
    End Sub
    Private Sub ButtonZ_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonZ.MouseDown
        ButtonTestfor(ButtonZBlocked, ButtonZ, e)
    End Sub
    Private Sub ButtonX_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonX.MouseDown
        ButtonTestfor(ButtonXBlocked, ButtonX, e)
    End Sub
    Private Sub ButtonC_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonC.MouseDown
        ButtonTestfor(ButtonCBlocked, ButtonC, e)
    End Sub
    Private Sub ButtonV_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonV.MouseDown
        ButtonTestfor(ButtonVBlocked, ButtonV, e)
    End Sub
    Private Sub ButtonB_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonB.MouseDown
        ButtonTestfor(ButtonBBlocked, ButtonB, e)
    End Sub
    Private Sub ButtonN_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonN.MouseDown
        ButtonTestfor(ButtonNBlocked, ButtonN, e)
    End Sub
    Private Sub ButtonM_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonM.MouseDown
        ButtonTestfor(ButtonMBlocked, ButtonM, e)
    End Sub
    Private Sub ButtonComma_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonComma.MouseDown
        ButtonTestfor(ButtonCommaBlocked, ButtonComma, e)
    End Sub
    Private Sub ButtonPeriod_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonPeriod.MouseDown
        ButtonTestfor(ButtonPeriodBlocked, ButtonPeriod, e)
    End Sub
    Private Sub ButtonSlash_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonSlash.MouseDown
        ButtonTestfor(ButtonSlashBlocked, ButtonSlash, e)
    End Sub
    Private Sub ButtonUp_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonUp.MouseDown
        ButtonTestfor(ButtonUpBlocked, ButtonUp, e)
    End Sub
    Private Sub ButtonLeftCtrl_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonLeftCtrl.MouseDown
        ButtonTestfor(ButtonLeftCtrlBlocked, ButtonLeftCtrl, e)
    End Sub
    Private Sub ButtonRightShift_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonRightShift.MouseDown
        ButtonTestfor(ButtonRightShiftBlocked, ButtonRightShift, e)
    End Sub
    Private Sub ButtonWin_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonWin.MouseDown
        ButtonTestfor(ButtonWinBlocked, ButtonWin, e)
    End Sub
    Private Sub ButtonLeftAlt_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonLeftAlt.MouseDown
        ButtonTestfor(ButtonLeftAltBlocked, ButtonLeftAlt, e)
    End Sub
    Private Sub ButtonSpace_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonSpace.MouseDown
        ButtonTestfor(ButtonSpaceBlocked, ButtonSpace, e)
    End Sub
    Private Sub ButtonRightAlt_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonRightAlt.MouseDown
        ButtonTestfor(ButtonRightAltBlocked, ButtonRightAlt, e)
    End Sub
    Private Sub ButtonRightCtrl_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonRightCtrl.MouseDown
        ButtonTestfor(ButtonRightCtrlBlocked, ButtonRightCtrl, e)
    End Sub
    Private Sub ButtonFn_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonFn.MouseDown
        ButtonTestfor(ButtonFnBlocked, ButtonFn, e)
    End Sub
    Private Sub ButtonLeft_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonLeft.MouseDown
        ButtonTestfor(ButtonLeftBlocked, ButtonLeft, e)
    End Sub
    Private Sub ButtonDown_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonDown.MouseDown
        ButtonTestfor(ButtonDownBlocked, ButtonDown, e)
    End Sub
    Private Sub ButtonRight_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonRight.MouseDown
        ButtonTestfor(ButtonRightBlocked, ButtonRight, e)
    End Sub
    Private Sub NumPad0_MouseDown(sender As Object, e As MouseEventArgs) Handles NumPad0.MouseDown
        ButtonTestfor(ButtonNumPad0Blocked, NumPad0, e)
    End Sub
    Private Sub NumPad1_MouseDown(sender As Object, e As MouseEventArgs) Handles NumPad1.MouseDown
        ButtonTestfor(ButtonNumPad1Blocked, NumPad1, e)
    End Sub
    Private Sub NumPad2_MouseDown(sender As Object, e As MouseEventArgs) Handles NumPad2.MouseDown
        ButtonTestfor(ButtonNumPad2Blocked, NumPad2, e)
    End Sub
    Private Sub NumPad3_MouseDown(sender As Object, e As MouseEventArgs) Handles NumPad3.MouseDown
        ButtonTestfor(ButtonNumPad3Blocked, NumPad3, e)
    End Sub
    Private Sub NumPad4_MouseDown(sender As Object, e As MouseEventArgs) Handles NumPad4.MouseDown
        ButtonTestfor(ButtonNumPad4Blocked, NumPad4, e)
    End Sub
    Private Sub NumPad5_MouseDown(sender As Object, e As MouseEventArgs) Handles NumPad5.MouseDown
        ButtonTestfor(ButtonNumPad5Blocked, NumPad5, e)
    End Sub
    Private Sub NumPad6_MouseDown(sender As Object, e As MouseEventArgs) Handles NumPad6.MouseDown
        ButtonTestfor(ButtonNumPad6Blocked, NumPad6, e)
    End Sub
    Private Sub NumPad7_MouseDown(sender As Object, e As MouseEventArgs) Handles NumPad7.MouseDown
        ButtonTestfor(ButtonNumPad7Blocked, NumPad7, e)
    End Sub
    Private Sub NumPad8_MouseDown(sender As Object, e As MouseEventArgs) Handles NumPad8.MouseDown
        ButtonTestfor(ButtonNumPad8Blocked, NumPad8, e)
    End Sub
    Private Sub NumPad9_MouseDown(sender As Object, e As MouseEventArgs) Handles NumPad9.MouseDown
        ButtonTestfor(ButtonNumPad9Blocked, NumPad9, e)
    End Sub
    Private Sub NumPadSubtract_MouseDown(sender As Object, e As MouseEventArgs) Handles NumPadSubtract.MouseDown
        ButtonTestfor(ButtonNumPadSubtractBlocked, NumPadSubtract, e)
    End Sub
    Private Sub NumPadAdd_MouseDown(sender As Object, e As MouseEventArgs) Handles NumPadAdd.MouseDown
        ButtonTestfor(ButtonNumPadAddBlocked, NumPadAdd, e)
    End Sub
    Private Sub NumPadMultiply_MouseDown(sender As Object, e As MouseEventArgs) Handles NumPadMultiply.MouseDown
        ButtonTestfor(ButtonNumPadMultiplyBlocked, NumPadMultiply, e)
    End Sub
    Private Sub NumPadEnter_MouseDown(sender As Object, e As MouseEventArgs) Handles NumPadEnter.MouseDown
        ButtonTestfor(ButtonNumPadEnterBlocked, NumPadEnter, e)
    End Sub
    Private Sub NumPadDecimal_MouseDown(sender As Object, e As MouseEventArgs) Handles NumPadDecimal.MouseDown
        ButtonTestfor(ButtonNumPadDecimalBlocked, NumPadDecimal, e)
    End Sub
    Private Sub NumPadDivide_MouseDown(sender As Object, e As MouseEventArgs) Handles NumPadDivide.MouseDown
        ButtonTestfor(ButtonNumPadDivideBlocked, NumPadDivide, e)
    End Sub
    Private Sub NumPadNumLock_MouseDown(sender As Object, e As MouseEventArgs) Handles NumPadNumLock.MouseDown
        ButtonTestfor(ButtonNumPadNumLockBlocked, NumPadNumLock, e)
    End Sub

    Private Sub Keyboard_Leave(sender As Object, e As EventArgs) Handles MyBase.Leave
        If AllBlocked Or Num1Blocked Or Num2Blocked Or Num3Blocked Or Num4Blocked Or Num5Blocked Or Num6Blocked Or Num7Blocked Or Num8Blocked Or
            Num9Blocked Or Num0Blocked Or ButtonABlocked Or ButtonBBlocked Or ButtonCBlocked Or ButtonDBlocked Or ButtonEBlocked Or ButtonFBlocked Or ButtonGBlocked Or
            ButtonHBlocked Or ButtonIBlocked Or ButtonJBlocked Or ButtonKBlocked Or ButtonLBlocked Or ButtonMBlocked Or ButtonNBlocked Or ButtonOBlocked Or
            ButtonPBlocked Or ButtonQBlocked Or ButtonRBlocked Or ButtonSBlocked Or ButtonTBlocked Or ButtonUBlocked Or ButtonVBlocked Or ButtonWBlocked Or
            ButtonXBlocked Or ButtonYBlocked Or ButtonZBlocked Or EscBlocked Or ButtonDelBlocked Or ButtonLeftCapsBlocked Or ButtonLeftCtrlBlocked Or ButtonTabBlocked Or
            ButtonLeftShiftBlocked Or ButtonRightShiftBlocked Or BackquoteBlocked Or ButtonGraveBlocked Or ButtonBackspaceBlocked Or ButtonAddBlocked Or
            ButtonHyphenBlocked Or ButtonCloseBracketsBlocked Or ButtonOpenBracketsBlocked Or ButtonBackSlashBlocked Or ButtonEnterBlocked Or ButtonQuotesBlocked Or
            ButtonSemicolenBlocked Or ButtonUpBlocked Or ButtonSlashBlocked Or ButtonPeriodBlocked Or ButtonCommaBlocked Or ButtonNumPad0Blocked Or ButtonNumPad1Blocked Or
            ButtonNumPad2Blocked Or ButtonNumPad3Blocked Or ButtonNumPad4Blocked Or ButtonNumPad5Blocked Or ButtonNumPad6Blocked Or ButtonNumPad7Blocked Or
            ButtonNumPad8Blocked Or ButtonNumPad9Blocked Or ButtonNumPadAddBlocked Or ButtonNumPadDecimalBlocked Or ButtonNumPadDivideBlocked Or ButtonNumPadEnterBlocked Or
            ButtonNumPadMultiplyBlocked Or ButtonNumPadNumLockBlocked Or ButtonNumPadSubtractBlocked Then
            Me.TopMost = True
        End If
    End Sub
    Private Sub ButtonFocus_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonFocus.MouseDown
        If e.Button = MouseButtons.Left Then
            If ButtonFocusToggle = True Then
                ButtonFocus.Cursor = Cursors.Default
                ButtonFocus.BackColor = Color.FromArgb(51, 51, 51)
                ButtonFocus.ForeColor = Color.FromArgb(221, 221, 221)
                ButtonFocusToggle = False
                Me.TopMost = False
            ElseIf ButtonFocusToggle = False Then
                ButtonFocus.BackColor = Color.Orange
                ButtonFocus.ForeColor = Color.Black
                Me.TopMost = True
                ButtonFocusToggle = True
            End If
        End If
    End Sub
    ' Lock and Unlock Buttons call functions
    Private Sub LockAll(sender As Object, e As MouseEventArgs) Handles LockAllButton.MouseClick
        If True Then
            Call Function() LockAll()
        End If
    End Sub
    Private Sub UnlockButton_Click(sender As Object, e As MouseEventArgs) Handles UnlockButton.MouseDown
        If True Then
            Call Function() UnlockAll()
        End If
    End Sub
    Private Sub ProgramToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProgramToolStripMenuItem.Click
        Program.Show()
    End Sub
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub
    Private Sub KeybindsToolStripMenuItem_MouseDown(sender As Object, e As EventArgs) Handles KeybindsToolStripMenuItem.Click
        Keybinds.Show()
    End Sub
    Public Enum Checkstate
        Unchecked = 0
        Checked = 1
        Indertiminate = 2
    End Enum
    Private Sub ProfileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProfileToolStripMenuItem.Click
        If ProfileToolStripMenuItem.CheckState = 1 Then
            Profile_Dock.Visible = True
        ElseIf ProfileToolStripMenuItem.CheckState = 0 Then
            Profile_Dock.Visible = False
        End If
    End Sub
End Class

