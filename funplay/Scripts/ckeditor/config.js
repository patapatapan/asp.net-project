/**
 * @license Copyright (c) 2003-2022, CKSource Holding sp. z o.o. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
    config.language = 'zh-TW';
    config.uiColor = '#AADC6E';
    config.basicEntities = false;
    config.entities = false;
    config.height = 500;
    //圖片默認顯示文字為空
    config.image_previewText = ' ';
    // 解決CKEditor圖片寬度自適應的問題 p img {    width: auto;    height: auto;    max - width: 100 %;}
    config.disallowedContent = 'img{ width, height }; img[width, height]';
};
