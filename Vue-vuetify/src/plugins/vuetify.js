import Vue from 'vue'
import Vuetify from 'vuetify/lib'
import light from './theme/light'
import dark from './theme/dark'

Vue.use(Vuetify)

import en from 'vuetify/lib/locale/en';
import zhHans from 'vuetify/lib/locale/zh-Hans';
import ja from 'vuetify/lib/locale/ja';

export default new Vuetify({
    lang: {
        locales: { en, zhHans, ja },
        current: 'zhHans',
    },
    theme: {
        themes: { light, dark },
    },
})