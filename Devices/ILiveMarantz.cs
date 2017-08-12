using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro.CrestronThread;
using ILiveLib;

namespace ILiveLib
{
    public class ILiveMarantz
    {
        /**
         Name of command,string
Main Zone Power Toggle,@PWR:0
Main Zone Power Off,@PWR:1
Main Zone Power On,@PWR:2
Power Off Global,@PWR:3
Main Zone Audio Att Toggle,@ATT:0
Main Zone Audio Att Off, @ATT:1
Main Zone Audio Att On, @ATT:2
Main Zone Audio Mute Toggle,@AMT:0
Main Zone Audio Mute Off,@AMT:1
Main Zone Audio Mute On,@AMT:2
Main Zone Video Mute Toggle,@VMT:0
Main Zone Video Mute Off,@VMT:1
Main Zone Video Mute On,@VMT:2
Main Zone Volume Up,@VOL:1
Main Zone Volume Down,@VOL:2
Main Zone Volume Up-Fast,@VOL:3
Main Zone Volume Down-Fast,@VOL:4
Main Zone Direct Volume -50,@VOL:0-50
Main Zone Tone Bass +5,@TOB:0+05
Main Zone Tone Bass Up,@TOB:1
Main Zone Tone Bass Down,@TOB:2
Main Zone Tone Treble +5,@TOT:0+05
Main Zone Tone Treble Up,@TOT:1
Main Zone Tone Treble Down,@TOT:2
Main Zone Source TV,@SRC:1
Main Zone Source DVD,@SRC:2
Main Zone Source VCR,@SRC:3
Main Zone Source DSS,@SRC:4
Main Zone Source AUX,@SRC:9
Main Zone Source CD/CDR,@SRC:C
Main Zone Source FM,@SRC:G
Main Zone Source AM,@SRC:H
Main Zone Source XM,@SRC:J
Main Zone Source Sirius,@SRC:K
Main Zone Source BD,@SRC:M
Main Zone Source M-Xport,@SRC:N
Main Zone 7.1 Ch Input Toggle,@71C:0
Main Zone 7.1 Ch Input Off,@71C:1
Main Zone 7.1 Ch Input On,@71C:2
Main Zone Input A/D Select Auto,@INP:0
Main Zone Input A/D Select Analog,@INP:1
Main Zone Input A/D Select Digital,@INP:2
Main Zone Input A/D Select HDMI,@INP:4
Main Zone Input A/D Select Toggle,@INP:F
Main Zone Speaker Select Toggle,@SPK:0
Main Zone Speaker Select A-Off,@SPK:1
Main Zone Speaker Select A-On,@SPK:2
Main Zone Speaker Select B-Off,@SPK:3
Main Zone Speaker Select B-On,@SPK:4
Main Zone HDMI Audio Enable,@HAM:1
Main Zone HDMI Audio Through,@HAM:2
Main Zone IP Converter Disable,@IPC:1
Main Zone IP Converter Enable,@IPC:2
Main Zone Display Toggle,@DIP:0
Main Zone Display Input,@DIP:1
Main Zone Display Surr,@DIP:2
Main Zone Display Auto Off,@DIP:3
Main Zone Display Off,@DIP:4
Main Zone OSD Toggle,@OSD:0
Main Zone OSD Off,@OSD:1
Main Zone OSD On,@OSD:2
Main Zone Sleep 30 mins,@SLP:0030
Main Zone Sleep Off,@SLP:1
Main Zone Menu Toggle,@MNU:0
Main Zone Menu Off,@MNU:1
Main Zone Menu On,@MNU:2
Main Zone Menu Enter,@MNU:3
Main Zone Menu Top,@MNU:4
Main Zone Cursor Up,@CUR:1
Main Zone Cursor Down,@CUR:2
Main Zone Cursor Left,@CUR:3
Main Zone Cursor Right,@CUR:4
Main Zone DC Trigger Off,@DCT:11
Main Zone DC Trigger On,@DCT:12
Main Zone Front Key Lock Toggle,@FKL:0
Main Zone Front Key Lock Off,@FKL:1
Main Zone Front Key Lock On,@FKL:2
Main Zone Surround Mode Auto,@SUR:00
Main Zone Surround Mode Stereo,@SUR:01
Main Zone Surround Mode Dolby,@SUR:02
Main Zone Surround Mode PL II(x) Movie,@SUR:03
Main Zone Surround Mode PL II(x) Music,@SUR:05
Main Zone Surround Mode PL II(x) Game,@SUR:07
Main Zone Surround Mode Neural,@SUR:0D
Main Zone Surround Mode DTS ES,@SUR:0E
Main Zone Surround Mode NEO:6 Cinema,@SUR:0F
Main Zone Surround Mode NEO:6 Music,@SUR:0G
Main Zone Surround Mode M-CH Movie,@SUR:0H
Main Zone Surround Mode CS II Cinema,@SUR:0I
Main Zone Surround Mode CS II Music,@SUR:0J
Main Zone Surround Mode CD II Mono,@SUR:0K
Main Zone Surround Mode Dolby VS,@SUR:0L
Main Zone Surround Mode DTS,@SUR:0M
Main Zone Surround Mode DD+PL II(x) Movie,@SUR:0O
Main Zone Surround Mode DD+PL II(x) Music,@SUR:0P
Main Zone Surround Mode Source Direct,@SUR:0T
Main Zone Surround Mode Pure Direct,@SUR:0U
Main Zone Surround Mode Dolby PL II(z),@SUR:0Z
Main Zone Surround Mode Multi CH (Music),@SUR:0h
Main Zone Surround Mode Up,@SUR:1
Main Zone Surround Mode Down,@SUR:2
Main Zone Audyssey Dynamic Vol. Off,@ADV:1
Main Zone Audyssey Dynamic Vol. Light,@ADV:2
Main Zone Audyssey Dynamic Vol. Medium,@ADV:2
Main Zone Audyssey Dynamic Vol. Heavy,@ADV:2
Main Zone Audyssey EQ Mode Preset 1,@EQM:1
Main Zone Audyssey EQ Mode Preset 2,@EQM:2
Main Zone Audyssey EQ Mode Front Curve,@EQM:3
Main Zone Audyssey EQ Mode Flat Curve,@EQM:4
Main Zone Audyssey EQ Mode Audyssey Curve,@EQM:5
Main Zone Audyssey EQ Mode Toggle,@EQM:6
Main Zone Audyssey EQ Mode Up,@EQM:7
Main Zone Audyssey EQ Mode Down,@        EQM:8
Main Zone Audyssey Dynamic EQ Off,@ADE:1
Main Zone Audyssey Dynamic EQ On,@ADE:2
Main Zone Audyssey Dynamic EQ Offset 0dB,@ADA:1
Main Zone Audyssey Dynamic EQ Offset 5dB,@ADA:2
Main Zone Audyssey Dynamic EQ Offset 10dB,@ADA:3
Main Zone Audyssey Dynamic EQ Offset 15dB,@ADA:4
Main Zone Dolby Headphone Mode Bypass,@DHM:0
Main Zone Dolby Headphone Mode DH1,@DHM:1
Main Zone Dolby Headphone Mode DH1 + PL II Movie,@DHM:2
Main Zone Dolby Headphone Mode DH1 + PL II Music,@DHM:3
Main Zone Night Mode Toggle,@NGT:0
Main Zone Night Mode Off,@NGT:1
Main Zone Night Mode On,@NGT:2
Main Zone Night Mode Auto,@NGT:3
Main Zone RE-EQ(HT-EQ) Toggle@REQ:0
Main Zone RE-EQ(HT-EQ) Off,@REQ:1
Main Zone RE-EQ(HT-EQ) On,@REQ:2
Main Zone Audio Channel (Bilingual) Toggle,@            BIL:0
Main Zone Audio Channel (Bilingual) Main,@BIL:1
Main Zone Audio Channel (Bilingual) Sub,@BIL:2
Main Zone Audio Channel (Bilingual) Main+Sub,@BIL:3
Main Zone M-DAX Toggle,@MDA:0
Main Zone M-DAX Off,@MDA:1
Main Zone M-DAX Low,@MDA:2
Main Zone M-DAX High,@MDA:3
Main Zone Lip Sync 50ms,@LIP:00050
Main Zone Lip Sync On,@LIP:1
Main Zone Lip Sync Down,@LIP:2
Main Zone Test Tone Toggle,@TTO:0
Main Zone Test Tone Off,        @TTO:1
Main Zone Test Tone On,@TTO:2
Main Zone Test Tone Next,@TTO:3
Main Zone Test Tone Prev,@TTO:4
Main Zone Ch. Select Toggle,@CSL:0
Main Zone Tuner Frequency 103.70,@TFQ:010370
Main Zone Tuner Frequency Up,@TFQ:1
Main Zone Tuner Frequency Down,@TFQ:2
Main Zone Tuner Frequency Auto-Up,@TFQ:3
Main Zone Tuner Frequency Auto-Down,@TFQ:4
Main Zone Tuner Preset 05,@TPR:005
Main Zone Tuner Preset Up,@TPR:1
Main Zone Tuner Preset Down,@        TPR:2
Main Zone Tuner Preset Scan Start,@TPR:3
Main Zone Tuner Preset Scan Stop,@TPR:4
Main Zone Tuner Mode Toggle,@TMD:0
Main Zone Tuner Mode Analog,@TMD:1
Main Zone Tuner Mode Auto,@TMD:2
Main Zone Tuner Preset Info Toggle,@TPI:0
Main Zone Tuner Preset Info Off,@TPI:1
Main Zone Tuner Preset Info On,@TPI:2
Main Zone Tuner Memo,@MEM:0
Main Zone Tuner Clear,@        CLR:0
Main Zone Sirius Parental Mode Off,@SPM:1
Main Zone Sirius Parental Mode On,@SPM:2
Main Zone XM & Sirius Category Search Toggle,@CAT:0
Main Zone XM & Sirius Category Search Ch. Up,@CAT:1
Main Zone XM & Sirius Category Search Ch. Down,@CAT:2
Main Zone XM & Sirius Category Search Category Next,@CAT:3
Main Zone XM & Sirius Category Search Category Prev.,@CAT:4
Zone A Power Toggle,@MPW:0
Zone A Power Off,@MPW:1
Zone A Power On,@MPW:2
Zone A Pre-out Audio Mute Toggle,@MAM:0
Zone A Pre-out Audio Mute Off,@MAM:1
Zone A Pre-out Audio Mute On,@MAM:2
Zone A Pre-out Volume -50dB,@MVL:0-50
Zone A Pre-out Volume Up,@MVL:1
Zone A Pre-out Volume Down,@MVL:2
Zone A Pre-out Volume Variable,@MVS:1
Zone A Pre-out Volume Fixed,@MVS:2
Zone A Pre-out Source TV,@MSC:1
Zone A Pre-out Source DVD,@MSC:2
Zone A Pre-out Source VCR,@MSC:3
Zone A Pre-out Source DSS,@MSC:4
Zone A Pre-out Source AUX,@MSC:9
Zone A Pre-out Source CD/CDR,@MSC:C
Zone A Pre-out Source FM,@MSC:G
Zone A Pre-out Source AM,@MSC:H
Zone A Pre-out Source XM,@MSC:J
Zone A Pre-out Source Sirius,@MSC:K
Zone A Pre-out Source BD, @MSC:M
Zone A Pre-out Source M-Xport,@MSC:N
Zone A Pre-out Sleep 30 min,@MSL:0030
Zone A Pre-out Sleep Off,@MSL:1
Zone A Pre-out Stereo/Mono Toggle,@MST:0
Zone A Pre-out Stereo,@MST:1
Zone A Pre-out Mono,@MST:2
Zone A Speaker Toggle,@MSP:0
Zone A Speaker Off,@MSP:1
Zone A Speaker On,@MSP:2
Zone A Speaker Volume -50dB,@MSV:0-50
Zone A Speaker Volume Up,@MSV:1
Zone A Speaker Volume Down,@MSV:2
Zone A Speaker Volume Variable,@MSS:1
Zone A Speaker Volume Fixed,@MSS:2
Zone A Speaker Audio Mute Toggle,@MSM:0
Zone A Speaker Audio Mute Off,@MSM:1
Zone A Speaker Audio Mute On,@MSM:2
Zone A Speaker Source TV,@SSC:1
Zone A Speaker Source DVD,@SSC:2
Zone A Speaker Source VCR,@SSC:3
Zone A Speaker Source DSS,@SSC:4
Zone A Speaker Source AUX,@SSC:9
Zone A Speaker Source CD/CDR,@SSC:C
Zone A Speaker Source FM,@SSC:G
Zone A Speaker Source AM,@SSC:H
Zone A Speaker Source Sirius,@SSC:I
Zone A Speaker Source BD,@SSC:M
Zone A Speaker Source M-Xport,@SSC:N
Zone A Tuner Frequency 103.70,@MTF:010370
Zone A Tuner Frequency Up,@MTF:1
Zone A Tuner Frequency Down,@MTF:2
Zone A Tuner Frequency Auto-Up,@MTF:3
Zone A Tuner Frequency Auto-Down,@MTF:4
Zone A Tuner Preset 05,@MTP:005
Zone A Tuner Preset Up,@MTP:1
Zone A Tuner Preset Down,@MTP:2
Zone A Tuner Preset Scan Start,@MTP:3
Zone A Tuner Preset Scan Stop,@MTP:4
Zone A Tuner Mode Toggle,@MTM:0
Zone A Tuner Mode Analog Mono,@MTM:1
Zone A Tuner Mode Analog Auto,@MTM:2
Zone B Power Toggle,@MPW=0
Zone B Power Off,@MPW=1
Zone B Power On,@MPW=2
Zone B Audio Mute Toggle,@MAM=0
Zone B Audio Mute Off,@MAM=1
Zone B Audio Mute On,@MAM=2
Zone B Pre-out Source TV,@MSC=1
Zone B Pre-out Source DVD,@MSC=2
Zone B Pre-out Source VCR,@MSC=3
Zone B Pre-out Source DSS,@MSC=4
Zone B Pre-out Source AUX,@MSC=9
Zone B Pre-out Source CD/CDR,@MSC=C
Zone B Pre-out Source FM,@MSC:=G
Zone B Pre-out Source AM,@MSC=H
Zone B Pre-out Source XM,@MSC=J
Zone B Pre-out Source Sirius,@MSC=K
Zone B Pre-out Source BD, @MSC=M
Zone B Pre-out Source M-Xport,@MSC=N
Zone B Sleep 30 mins,@MSL=0030
Zone B Sleep Off,@MSL=1
Zone B Tuner Frequency 103.70,@MTF=010370
Zone B Tuner Frequency Up,@MTF=1
Zone B Tuner Frequency Down,@MTF=2
Zone B Tuner Preset 05,@MTP=005
Zone B Tuner Preset Up,@MTP=1
Zone B Tuner Preset Down,@MTP=2
Zone B Tuner Preset Scan Start,@MTP=3
Zone B Tuner Preset Scan Stop,@MTP=4

Status Feedback:

Main Zone Request Power Status,@PWR:?

Main Zone Response Power Status Off,@PWR:1

Main Zone Response Power Status On,@PWR:2

Zone A Request Power Status,@MPW:?

Zone A Response Power Status Off,@MPW:1

Zone A Response Power Status On,@MPW:2

Zone B Request Power Status,@MPW=?

Zone B Response Power Status Off,@MPW=1

Zone B Response Power Status ON,@MPW=2

 

Sirius Channels:

Main Zone Tuner Frequency Sirius 001,@TFQ:000001

Main Zone Tuner Frequency Sirius 002,@TFQ:000002

Main Zone Tuner Frequency Sirius 003,@TFQ:000003

Main Zone Tuner Frequency Sirius 004,@TFQ:000004

Main Zone Tuner Frequency Sirius 005,@TFQ:000005

Main Zone Tuner Frequency Sirius 006,@TFQ:000006

Main Zone Tuner Frequency Sirius 007,@TFQ:000007

Main Zone Tuner Frequency Sirius 008,@TFQ:000008

Main Zone Tuner Frequency Sirius 009,@TFQ:000009

Main Zone Tuner Frequency Sirius 010,@TFQ:000010

Main Zone Tuner Frequency Sirius 011,@TFQ:000011

Main Zone Tuner Frequency Sirius 012,@TFQ:000012

Main Zone Tuner Frequency Sirius 013,@TFQ:000013

Main Zone Tuner Frequency Sirius 014,@TFQ:000014

Main Zone Tuner Frequency Sirius 015,@TFQ:000015

Main Zone Tuner Frequency Sirius 016,@TFQ:000016

Main Zone Tuner Frequency Sirius 017,@TFQ:000017

Main Zone Tuner Frequency Sirius 018,@TFQ:000018

Main Zone Tuner Frequency Sirius 019,@TFQ:000019

Main Zone Tuner Frequency Sirius 020,@TFQ:000020

Main Zone Tuner Frequency Sirius 021,@TFQ:000021

Main Zone Tuner Frequency Sirius 022,@TFQ:000022

Main Zone Tuner Frequency Sirius 023,@TFQ:000023

Main Zone Tuner Frequency Sirius 024,@TFQ:000024

Main Zone Tuner Frequency Sirius 025,@TFQ:000025

Main Zone Tuner Frequency Sirius 026,@TFQ:000026

Main Zone Tuner Frequency Sirius 027,@TFQ:000027

Main Zone Tuner Frequency Sirius 028,@TFQ:000028

Main Zone Tuner Frequency Sirius 029,@TFQ:000029

Main Zone Tuner Frequency Sirius 030,@TFQ:000030

Main Zone Tuner Frequency Sirius 031,@TFQ:000031

Main Zone Tuner Frequency Sirius 032,@TFQ:000032

Main Zone Tuner Frequency Sirius 033,@TFQ:000033

Main Zone Tuner Frequency Sirius 034,@TFQ:000034

Main Zone Tuner Frequency Sirius 035,@TFQ:000035

Main Zone Tuner Frequency Sirius 036,@TFQ:000036

Main Zone Tuner Frequency Sirius 037,@TFQ:000037

Main Zone Tuner Frequency Sirius 038,@TFQ:000038

Main Zone Tuner Frequency Sirius 039,@TFQ:000039

Main Zone Tuner Frequency Sirius 040,@TFQ:000040

Main Zone Tuner Frequency Sirius 041,@TFQ:000041

Main Zone Tuner Frequency Sirius 042,@TFQ:000042

Main Zone Tuner Frequency Sirius 043,@TFQ:000043

Main Zone Tuner Frequency Sirius 044,@TFQ:000044

Main Zone Tuner Frequency Sirius 045,@TFQ:000045

Main Zone Tuner Frequency Sirius 046,@TFQ:000046

Main Zone Tuner Frequency Sirius 047,@TFQ:000047

Main Zone Tuner Frequency Sirius 048,@TFQ:000048

Main Zone Tuner Frequency Sirius 049,@TFQ:000049

Main Zone Tuner Frequency Sirius 050,@TFQ:000050

Main Zone Tuner Frequency Sirius 051,@TFQ:000051

Main Zone Tuner Frequency Sirius 052,@TFQ:000052

Main Zone Tuner Frequency Sirius 053,@TFQ:000053

Main Zone Tuner Frequency Sirius 054,@TFQ:000054

Main Zone Tuner Frequency Sirius 055,@TFQ:000055

Main Zone Tuner Frequency Sirius 056,@TFQ:000056

Main Zone Tuner Frequency Sirius 057,@TFQ:000057

Main Zone Tuner Frequency Sirius 058,@TFQ:000058

Main Zone Tuner Frequency Sirius 059,@TFQ:000059

Main Zone Tuner Frequency Sirius 060,@TFQ:000060

Main Zone Tuner Frequency Sirius 061,@TFQ:000061

Main Zone Tuner Frequency Sirius 062,@TFQ:000062

Main Zone Tuner Frequency Sirius 063,@TFQ:000063

Main Zone Tuner Frequency Sirius 064,@TFQ:000064

Main Zone Tuner Frequency Sirius 065,@TFQ:000065

Main Zone Tuner Frequency Sirius 066,@TFQ:000066

Main Zone Tuner Frequency Sirius 067,@TFQ:000067

Main Zone Tuner Frequency Sirius 068,@TFQ:000068

Main Zone Tuner Frequency Sirius 069,@TFQ:000069

Main Zone Tuner Frequency Sirius 070,@TFQ:000070

Main Zone Tuner Frequency Sirius 071,@TFQ:000071

Main Zone Tuner Frequency Sirius 072,@TFQ:000072

Main Zone Tuner Frequency Sirius 073,@TFQ:000073

Main Zone Tuner Frequency Sirius 074,@TFQ:000074

Main Zone Tuner Frequency Sirius 075,@TFQ:000075

Main Zone Tuner Frequency Sirius 076,@TFQ:000076

Main Zone Tuner Frequency Sirius 077,@TFQ:000077

Main Zone Tuner Frequency Sirius 078,@TFQ:000078

Main Zone Tuner Frequency Sirius 079,@TFQ:000079

Main Zone Tuner Frequency Sirius 080,@TFQ:000080

Main Zone Tuner Frequency Sirius 081,@TFQ:000081

Main Zone Tuner Frequency Sirius 082,@TFQ:000082

Main Zone Tuner Frequency Sirius 083,@TFQ:000083

Main Zone Tuner Frequency Sirius 084,@TFQ:000084

Main Zone Tuner Frequency Sirius 085,@TFQ:000085

Main Zone Tuner Frequency Sirius 086,@TFQ:000086

Main Zone Tuner Frequency Sirius 087,@TFQ:000087

Main Zone Tuner Frequency Sirius 088,@TFQ:000088

Main Zone Tuner Frequency Sirius 089,@TFQ:000089

Main Zone Tuner Frequency Sirius 090,@TFQ:000090

Main Zone Tuner Frequency Sirius 091,@TFQ:000091

Main Zone Tuner Frequency Sirius 092,@TFQ:000092

Main Zone Tuner Frequency Sirius 093,@TFQ:000093

Main Zone Tuner Frequency Sirius 094,@TFQ:000094

Main Zone Tuner Frequency Sirius 095,@TFQ:000095

Main Zone Tuner Frequency Sirius 096,@TFQ:000096

Main Zone Tuner Frequency Sirius 097,@TFQ:000097

Main Zone Tuner Frequency Sirius 098,@TFQ:000098

Main Zone Tuner Frequency Sirius 099,@TFQ:000099

Main Zone Tuner Frequency Sirius 100,@TFQ:000100

Main Zone Tuner Frequency Sirius 101,@TFQ:000101

Main Zone Tuner Frequency Sirius 102,@TFQ:000102

Main Zone Tuner Frequency Sirius 103,@TFQ:000103

Main Zone Tuner Frequency Sirius 104,@TFQ:000104

Main Zone Tuner Frequency Sirius 105,@TFQ:000105

Main Zone Tuner Frequency Sirius 106,@TFQ:000106

Main Zone Tuner Frequency Sirius 107,@TFQ:000107

Main Zone Tuner Frequency Sirius 108,@TFQ:000108

Main Zone Tuner Frequency Sirius 109,@TFQ:000109

Main Zone Tuner Frequency Sirius 110,@TFQ:000110

Main Zone Tuner Frequency Sirius 111,@TFQ:000111

Main Zone Tuner Frequency Sirius 112,@TFQ:000112

Main Zone Tuner Frequency Sirius 113,@TFQ:000113

Main Zone Tuner Frequency Sirius 114,@TFQ:000114

Main Zone Tuner Frequency Sirius 115,@TFQ:000115

Main Zone Tuner Frequency Sirius 116,@TFQ:000116

Main Zone Tuner Frequency Sirius 117,@TFQ:000117

Main Zone Tuner Frequency Sirius 118,@TFQ:000118

Main Zone Tuner Frequency Sirius 119,@TFQ:000119

Main Zone Tuner Frequency Sirius 120,@TFQ:000120

Main Zone Tuner Frequency Sirius 121,@TFQ:000121

Main Zone Tuner Frequency Sirius 122,@TFQ:000122

Main Zone Tuner Frequency Sirius 123,@TFQ:000123

Main Zone Tuner Frequency Sirius 124,@TFQ:000124

Main Zone Tuner Frequency Sirius 125,@TFQ:000125

Main Zone Tuner Frequency Sirius 126,@TFQ:000126

Main Zone Tuner Frequency Sirius 127,@TFQ:000127

Main Zone Tuner Frequency Sirius 128,@TFQ:000128

Main Zone Tuner Frequency Sirius 129,@TFQ:000129

Main Zone Tuner Frequency Sirius 130,@TFQ:000130

Main Zone Tuner Frequency Sirius 131,@TFQ:000131

Main Zone Tuner Frequency Sirius 132,@TFQ:000132

Main Zone Tuner Frequency Sirius 133,@TFQ:000133

Main Zone Tuner Frequency Sirius 134,@TFQ:000134

Main Zone Tuner Frequency Sirius 135,@TFQ:000135

Main Zone Tuner Frequency Sirius 136,@TFQ:000136

Main Zone Tuner Frequency Sirius 137,@TFQ:000137

Main Zone Tuner Frequency Sirius 138,@TFQ:000138

Main Zone Tuner Frequency Sirius 139,@TFQ:000139

Main Zone Tuner Frequency Sirius 140,@TFQ:000140

Main Zone Tuner Frequency Sirius 141,@TFQ:000141

Main Zone Tuner Frequency Sirius 142,@TFQ:000142

Main Zone Tuner Frequency Sirius 143,@TFQ:000143

Main Zone Tuner Frequency Sirius 144,@TFQ:000144

Main Zone Tuner Frequency Sirius 145,@TFQ:000145

Main Zone Tuner Frequency Sirius 146,@TFQ:000146

Main Zone Tuner Frequency Sirius 147,@TFQ:000147

Main Zone Tuner Frequency Sirius 148,@TFQ:000148

Main Zone Tuner Frequency Sirius 149,@TFQ:000149

Main Zone Tuner Frequency Sirius 150,@TFQ:000150

Main Zone Tuner Frequency Sirius 151,@TFQ:000151

Main Zone Tuner Frequency Sirius 152,@TFQ:000152

Main Zone Tuner Frequency Sirius 153,@TFQ:000153

Main Zone Tuner Frequency Sirius 154,@TFQ:000154

Main Zone Tuner Frequency Sirius 155,@TFQ:000155

Main Zone Tuner Frequency Sirius 156,@TFQ:000156

Main Zone Tuner Frequency Sirius 157,@TFQ:000157

Main Zone Tuner Frequency Sirius 158,@TFQ:000158

Main Zone Tuner Frequency Sirius 159,@TFQ:000159

Main Zone Tuner Frequency Sirius 160,@TFQ:000160

Main Zone Tuner Frequency Sirius 161,@TFQ:000161

Main Zone Tuner Frequency Sirius 162,@TFQ:000162

Main Zone Tuner Frequency Sirius 163,@TFQ:000163

Main Zone Tuner Frequency Sirius 164,@TFQ:000164

Main Zone Tuner Frequency Sirius 165,@TFQ:000165

Main Zone Tuner Frequency Sirius 166,@TFQ:000166

Main Zone Tuner Frequency Sirius 167,@TFQ:000167

Main Zone Tuner Frequency Sirius 168,@TFQ:000168

Main Zone Tuner Frequency Sirius 169,@TFQ:000169

Main Zone Tuner Frequency Sirius 170,@TFQ:000170

Main Zone Tuner Frequency Sirius 171,@TFQ:000171

Main Zone Tuner Frequency Sirius 172,@TFQ:000172

Main Zone Tuner Frequency Sirius 173,@TFQ:000173

Main Zone Tuner Frequency Sirius 174,@TFQ:000174

Main Zone Tuner Frequency Sirius 175,@TFQ:000175

Main Zone Tuner Frequency Sirius 176,@TFQ:000176

Main Zone Tuner Frequency Sirius 177,@TFQ:000177

Main Zone Tuner Frequency Sirius 178,@TFQ:000178

Main Zone Tuner Frequency Sirius 179,@TFQ:000179

Main Zone Tuner Frequency Sirius 180,@TFQ:000180

Main Zone Tuner Frequency Sirius 181,@TFQ:000181

Main Zone Tuner Frequency Sirius 182,@TFQ:000182

Main Zone Tuner Frequency Sirius 183,@TFQ:000183

Main Zone Tuner Frequency Sirius 184,@TFQ:000184

Main Zone Tuner Frequency Sirius 185,@TFQ:000185

Main Zone Tuner Frequency Sirius 186,@TFQ:000186

Main Zone Tuner Frequency Sirius 187,@TFQ:000187

Main Zone Tuner Frequency Sirius 188,@TFQ:000188

Main Zone Tuner Frequency Sirius 189,@TFQ:000189

Main Zone Tuner Frequency Sirius 190,@TFQ:000190

Main Zone Tuner Frequency Sirius 191,@TFQ:000191

Main Zone Tuner Frequency Sirius 192,@TFQ:000192

Main Zone Tuner Frequency Sirius 193,@TFQ:000193

Main Zone Tuner Frequency Sirius 194,@TFQ:000194

Main Zone Tuner Frequency Sirius 195,@TFQ:000195

Main Zone Tuner Frequency Sirius 196,@TFQ:000196

Main Zone Tuner Frequency Sirius 197,@TFQ:000197

Main Zone Tuner Frequency Sirius 198,@TFQ:000198

Main Zone Tuner Frequency Sirius 199,@TFQ:000199

Main Zone Tuner Frequency Sirius 200,@TFQ:000200

Main Zone Tuner Frequency Sirius 201,@TFQ:000201

Main Zone Tuner Frequency Sirius 202,@TFQ:000202

Main Zone Tuner Frequency Sirius 203,@TFQ:000203

Main Zone Tuner Frequency Sirius 204,@TFQ:000204

Main Zone Tuner Frequency Sirius 205,@TFQ:000205

Main Zone Tuner Frequency Sirius 206,@TFQ:000206

Main Zone Tuner Frequency Sirius 207,@TFQ:000207

Main Zone Tuner Frequency Sirius 208,@TFQ:000208

Main Zone Tuner Frequency Sirius 209,@TFQ:000209

Main Zone Tuner Frequency Sirius 210,@TFQ:000210

Main Zone Tuner Frequency Sirius 211,@TFQ:000211

Main Zone Tuner Frequency Sirius 212,@TFQ:000212

Main Zone Tuner Frequency Sirius 213,@TFQ:000213

Main Zone Tuner Frequency Sirius 214,@TFQ:000214

Main Zone Tuner Frequency Sirius 215,@TFQ:000215

Main Zone Tuner Frequency Sirius 216,@TFQ:000216

Main Zone Tuner Frequency Sirius 217,@TFQ:000217

Main Zone Tuner Frequency Sirius 218,@TFQ:000218

Main Zone Tuner Frequency Sirius 219,@TFQ:000219

Main Zone Tuner Frequency Sirius 220,@TFQ:000220 
         */
        /*
         Main Zone Audio Mute Toggle,@AMT:0
Main Zone Audio Mute Off,@AMT:1
Main Zone Audio Mute On,@AMT:2
Main Zone Video Mute Toggle,@VMT:0
Main Zone Video Mute Off,@VMT:1
Main Zone Video Mute On,@VMT:2
Main Zone Volume Up,@VOL:1
Main Zone Volume Down,@VOL:2
Main Zone Volume Up-Fast,@VOL:3
Main Zone Volume Down-Fast,@VOL:4
         * 
        Main Zone Source TV,@SRC:1
Main Zone Source DVD,@SRC:2
Main Zone Source VCR,@SRC:3
Main Zone Source DSS,@SRC:4
Main Zone Source AUX,@SRC:9
Main Zone Source CD/CDR,@SRC:C
Main Zone Source FM,@SRC:G
Main Zone Source AM,@SRC:H
Main Zone Source XM,@SRC:J
Main Zone Source Sirius,@SRC:K
Main Zone Source BD,@SRC:M
Main Zone Source M-Xport,@SRC:N
         */
        INetPortDevice server = null;
        public ILiveMarantz(INetPortDevice port)
        {
            try
            {
                this.server = port;
            }
            catch (Exception ex)
            {
                ILiveDebug.Instance.WriteLine(ex.Message);
            }
        }
        public void Power(bool ison)
        {
            if (ison)
            {
                this.Send("ZMON\r");
            }
            else
            {
                this.Send("ZMOFF\r");
            }
        }
        public void SetVol(int vol)
        {
            this.Send("MV" + vol.ToString("D2")+"\r");
        }
        public void VolUp()
        {
            this.Send("ZMUP\r");
        }
        public void VolDown()
        {
            this.Send("ZMDOWN\r");

        }
        public void Mute(bool f)
        {
            if (f)
            {
                this.Send("MUON\r");
            }
            else
            {
                this.Send("MUOFF\r");
            }
        }
        public void SwitchHDMI(int i)
        {
            switch (i)
            {
                case 1:
                    this.Send("SIDVD\r");
                    break;
                case 2:
                    this.Send("SISAT/CBL\r");
                    break;
                case 3:
                    this.Send("SIBD\r");
                    break;
                case 4:
                    this.Send("SIGAME\r");
                    break;
                default:
                    break;
            }
        }
        private void Send(string data)
        {
            this.server.Send(data);

            Thread.Sleep(200);
        }
    }
}