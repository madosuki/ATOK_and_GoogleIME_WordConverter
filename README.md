# ATOK_and_GoogleIME_WordConverter
コマンドラインアプリケーションです。
ATOKとGoogle日本語入力の辞書ツールで出力したテキストファイルを相互変換するプログラムです。  
Windowsではtxtファイルをドラッグするだけで使えます。Windows依存のコードは使っていないのでMonoでも動作します。  
複数ファイルを一度に変換する場合はATOKとGoogle日本語入力のファイルを混在しないでください。  
成功すれば引数として渡されたファイルのパスに"converted_元のファイル名"といった感じのファイルが出力されます。  
ATOKの場合はShiftJISを前提にしていますのでATOK本体からエクスポートする場合はUNICODEのチェックを入れないでください。対応しようと思えばできますが面倒なので気が乗った時にします。  
また変換時に対応する品詞が存在しないものはコンバート後のファイルに含まれません。その一覧は下記にありますのでどうぞ。
  
|Google日本語入力からATOK用に変換する際に変換不能の為、失われる品詞一覧|
|:---|
|句読点|
|抑制単語|
  

|ATOKからGoogle日本語入力用に変換する際に変換不能の為、失われる品詞一覧|
|:---|
|ワ行五段音便|
|ナ変動詞|
|カ行上二段|
|ガ行上二段|
|タ行上二段|
|ダ行上二段|
|ハ行上二段|
|バ行上二段|
|マ行上二段|
|ヤ行上二段|
|ラ行上二段|
|カ行下二段|
|ガ行下二段|
|サ行下二段|
|ザ行下二段|
|タ行下二段|
|ダ行下二段|
|ナ行下二段|
|ハ行下二段|
|バ行下二段|
|マ行下二段|
|ヤ行下二段|
|ラ行下二段|
|ワ行下二段|