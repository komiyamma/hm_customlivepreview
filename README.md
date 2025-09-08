# HmCustomLivePreview

![HmCustomLivePreview v2.0.1](https://img.shields.io/badge/HmCustomLivePreview-v2.0.1-6479ff.svg)
[![Apache 2.0](https://img.shields.io/badge/license-Apache_2.0-blue.svg?style=flat)](LICENSE)
![Hidemaru 8.73](https://img.shields.io/badge/Hidemaru-v8.73-6479ff.svg)

[https://秀丸マクロ.net/?page=nobu_tool_hm_customlivepreview](https://秀丸マクロ.net/?page=nobu_tool_hm_customlivepreview)

## 概要

秀丸エディタ上で編集中のテキストを、独自の変換ロジックを用いてリアルタイムにHTMLプレビューするためのコンポーネントです。

編集中のテキストを、指定したJavaScriptファイルを使ってHTMLに変換し、「HTMLソース」と「ブラウザでのレンダリング結果」をリアルタイムでプレビューし続けることができます。
サンプルでは、`marked.js` を利用してMarkdownをプレビューする例が含まれています。

## 特徴

- **リアルタイムプレビュー**: 編集中のテキストがリアルタイムでHTMLとしてプレビューされます。
- **高いカスタマイズ性**: 変換ロジックをJavaScriptで記述できるため、Markdown、reStructuredTextなど、様々な形式に対応可能です。
- **HTMLソースの確認**: 生成されたHTMLソースコードを直接確認できます。
- **軽量な動作**: 秀丸エディタのコンポーネントとして軽快に動作します。

## 仕組み

本コンポーネントは、以下の要素で構成されています。

1.  **C++ DLL (`hmJS.dll`)**: 秀丸エディタ本体との連携、UIの管理、テキストの取得など、プラグインの核となる部分です。
2.  **C#ライブラリ (`hmJSStaticLib`)**: `ClearScript` を利用してJavaScriptエンジンをホストし、C++側から受け取ったテキストをJavaScriptに渡して変換処理を実行します。
3.  **JavaScriptファイル**: 実際にテキストをHTMLに変換するロジックを記述します。ユーザーはこのファイルを編集・指定することで、変換方法を自由にカスタマイズできます。
4.  **HTML Agility Pack**: C#側でHTMLを解析・操作するために利用されています。

## 技術スタック

- **C++**: 秀丸エディタとの連携
- **C# (.NET Framework)**: メインロジック、JavaScriptのホスティング
- **Microsoft ClearScript**: .NETアプリケーションにスクリプト機能を追加するためのライブラリ
- **Html Agility Pack**: HTMLの解析・操作ライブラリ
- **JavaScript**: ユーザーによる変換ロジックの実装

## カスタマイズ

テキストからHTMLへの変換方法は、JavaScriptファイルを編集することでカスタマイズできます。

`sample` フォルダに含まれる `HmCustomLivePreviewMD.js` と `HmCustomLivePreviewMD.mac` を参考に、ご自身のマクロとJSファイルを作成してください。
マクロファイル内で、使用するJavaScriptファイルのパスを指定することで、独自のプレビューアとして利用できます。

## ビルド

1.  `HmCustomLivePreview.src\hmCustomLivePreview.sln` を Visual Studio で開きます。（VS2017推奨）
2.  ソリューションをビルドします。
3.  `HmCustomLivePreview.src\Release` フォルダ内に `hmJS.dll` が生成されます。

この `hmJS.dll` を秀丸エディタのコンポーネントとして登録し、マクロから利用してください。
