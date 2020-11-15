import { FuseNavigation } from "@fuse/types";

export const navigation: FuseNavigation[] = [
    {
        id: "principal",
        title: "",
        type: "group",
        icon: "web",
        children: [
            {
                id: "music",
                title: "music",
                type: "item",
                icon: "keyboard_arrow_right",
                url: "/page/music",
            },
        ],
    },
];
