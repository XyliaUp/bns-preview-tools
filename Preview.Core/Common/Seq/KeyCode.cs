using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Common.Seq;
public enum KeyCode
{
	None,

	N0,

	N1,

	N2,

	N3,

	N4,

	N5,

	N6,

	N7,

	N8,

	N9,

	[Signal("numpad-0")]
	Numpad0,

	[Signal("numpad-1")]
	Numpad1,

	[Signal("numpad-2")]
	Numpad2,

	[Signal("numpad-3")]
	Numpad3,

	[Signal("numpad-4")]
	Numpad4,

	[Signal("numpad-5")]
	Numpad5,

	[Signal("numpad-6")]
	Numpad6,

	[Signal("numpad-7")]
	Numpad7,

	[Signal("numpad-8")]
	Numpad8,

	[Signal("numpad-9")]
	Numpad9,

	F1,

	F2,

	F3,

	F4,

	F5,

	F6,

	F7,

	F8,

	F9,

	F10,

	F11,

	F12,

	A,

	B,

	C,

	D,

	E,

	F,

	G,

	H,

	I,

	J,

	K,

	L,

	M,

	N,

	O,

	P,

	Q,

	R,

	S,

	T,

	U,

	V,

	W,

	X,

	Y,

	Z,

	[Signal("back-space")]
	BackSpace,

	Tab,

	[Signal("caps-lock")]
	CapsLock,

	Space,

	[Signal("page-up")]
	PageUp,

	[Signal("page-down")]
	PageDown,

	End,

	Home,

	Left,

	Up,

	Right,

	Down,

	Insert,

	Delete,

	Multiply,

	Add,

	Subtract,

	Decimal,

	Divide,

	[Signal("num-lock")]
	NumLock,

	Shift,

	Control,

	Alt,

	Semicolon,

	Equal,

	Minus,

	Comma,

	Period,

	Slash,

	Tilde,

	[Signal("left-square-bracket")]
	LeftSquareBracket,

	[Signal("back-slash")]
	BackSlash,

	[Signal("right-square-bracket")]
	RightSquareBracket,

	[Signal("single-quatation")]
	SingleQuatation,

	[Signal("mouse-left-button")]
	MouseLeftButton,

	[Signal("mouse-middle-button")]
	MouseMiddleButton,

	[Signal("mouse-right-button")]
	MouseRightButton,

	[Signal("joybtn-a")]
	JoybtnA,

	[Signal("joybtn-b")]
	JoybtnB,

	[Signal("joybtn-x")]
	JoybtnX,

	[Signal("joybtn-y")]
	JoybtnY,

	[Signal("joybtn-dpad-up")]
	JoybtnDpadUp,

	[Signal("joybtn-dpad-down")]
	JoybtnDpadDown,

	[Signal("joybtn-dpad-left")]
	JoybtnDpadLeft,

	[Signal("joybtn-dpad-right")]
	JoybtnDpadRight,

	[Signal("joybtn-leftstick-up")]
	JoybtnLeftstickUp,

	[Signal("joybtn-leftstick-down")]
	JoybtnLeftstickDown,

	[Signal("joybtn-leftstick-left")]
	JoybtnLeftstickLeft,

	[Signal("joybtn-leftstick-right")]
	JoybtnLeftstickRight,

	[Signal("joybtn-rightstick-up")]
	JoybtnRightstickUp,

	[Signal("joybtn-rightstick-down")]
	JoybtnRightstickDown,

	[Signal("joybtn-rightstick-left")]
	JoybtnRightstickLeft,

	[Signal("joybtn-rightstick-right")]
	JoybtnRightstickRight,

	[Signal("joybtn-leftshoulder")]
	JoybtnLeftshoulder,

	[Signal("joybtn-rightshoulder")]
	JoybtnRightshoulder,

	[Signal("joybtn-start")]
	JoybtnStart,

	[Signal("joybtn-back")]
	JoybtnBack,

	[Signal("joybtn-leftthumbstick")]
	JoybtnLeftthumbstick,

	[Signal("joybtn-rightthumbstick")]
	JoybtnRightthumbstick,

	[Signal("joybtn-lefttrigger")]
	JoybtnLefttrigger,

	[Signal("joybtn-righttrigger")]
	JoybtnRighttrigger,

	[Signal("joybtn-function1")]
	JoybtnFunction1,

	[Signal("joybtn-function2")]
	JoybtnFunction2,

	Escape,

	Enter,

	[Signal("shift-joypad-replaceable")]
	ShiftJoypadReplaceable,

	[Signal("shift-dash-forward-joypad-replaceable")]
	ShiftDashForwardJoypadReplaceable,

	[Signal("control-joypad-replaceable")]
	ControlJoypadReplaceable,

	[Signal("alt-joypad-replaceable")]
	AltJoypadReplaceable,

	[Signal("mouse-left-button-joypad-replaceable")]
	MouseLeftButtonJoypadReplaceable,

	[Signal("mouse-right-button-joypad-replaceable")]
	MouseRightButtonJoypadReplaceable,

	[Signal("escape-joypad-replaceable")]
	EscapeJoypadReplaceable,
}