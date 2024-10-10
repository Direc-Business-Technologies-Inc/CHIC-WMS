(function constantSetter(window) {
    const setGlobalConstant = (name, value) => {
        const fnval = value;
        const fnName = name;
        if (typeof fnName != "string") throw "Variable name must be a string";
        Object.defineProperty(window, fnName, {
            value: fnval,
            configurable: false,
            writable: false
        });
    }

    Object.defineProperty(window, 'setGlobalConstant', {
        value: setGlobalConstant,
        configurable: false,
        writable: false
    });

    const setObjConst = (name, value, object) => {
        const fnval = value;
        const fnName = name;
        if (typeof fnName != "string") throw "Variable name must be a string";
        Object.defineProperty(object, fnName, {
            value: fnval,
            configurable: false,
            writable: false
        });
    }

    window.setGlobalConstant("setObjConst", setObjConst);

})(window);

(function devThings(window) {
    window.setGlobalConstant("DEV", {});
})(window);

(function Regex(window) {
    const DEV = window.DEV;
    const Regex = (superclass) => class extends superclass {
        static get html_id() {
            return /^[A-Za-z]+[\w\-\:\.]*$/g;
        }
    }

    DEV.Regex = Regex;

})(window);


+(function nodeTreeBuilder(window) {

    const sampleData = [
        {
            tag: "DIV",
            attr: {
                id: "NTB-sample-root"
            },
            childs: [
                {
                    tag: "TABLE",
                    attr: {
                        class: "card-datatable"
                    }
                }
            ]
        }
    ]

    class NodeTreeBuilder {

        static build(nodeTree) {
            const tree = nodeTree;
            return this._traverseObject(tree);
        }

        static _traverseObject(nodeArr) {
            const main_this = this;
            const arr = nodeArr, arrType = typeof nodeArr;
            if (Array.isArray(nodeArr)) {
                const nodes = arr.map((el => {
                    const node = this._createNode(el.tag, el.attrs);
                    if (typeof el.listeners === "object") this._addListener(node, el.listeners);
                    if (el.text) node.textContent = el.text;
                    if (typeof el.childs != "undefined" && Array.isArray(el.childs)) {
                        const childs = this._traverseObject(el.childs);
                        node.append(...childs);
                    }
                    return node;
                }).bind(this));
                return nodes;
            }
            const nodeObj = nodeArr;
            const node = this._createNode(nodeObj.tag, nodeObj.attrs);
            if (typeof nodeObj.listeners === "object") this._addListener(node, nodeObj.listeners);
            if (nodeObj.text) node.textContent = nodeObj.text;
            if (typeof nodeObj.childs == "object") {
                const childs = this._traverseObject(nodeObj.childs);
                node.append(...childs);
            }
            return node;
        }

        static _createNode = function (tag, attrs = {}) {
            const node = document.createElement(tag);
            this._addAttrs(node, attrs);
            return node;
        }

        static _addAttrs(node, attrs) {
            for (let [key, value] of Object.entries(attrs))
                node.setAttribute(key, value);
        }

        static _addListener(node, listeners) {
            for (let [key, value] of Object.entries(listeners))
                node.addEventListener(key, value);
        }

    }

    window.NodeTreeBuilder = NodeTreeBuilder;

})(window);

+(function breadcrumbs(window) {

    const NodeTreeBuilder = window.NodeTreeBuilder;

    class BreadcrumbsDefaultSettings {
        static separateContainer = false;
        static separator = {
            tag: "SPAN",
            attrs: {
                class: "text-muted mx-1 fw-light"
            },
            text: "/"
        };
        static entries = {
            active: {
                tag: "SPAN",
                attrs: {
                    class: "d-inline"
                }
            },
            inactive: {
                tag: "A",
                attrs: {
                    class: "text-muted fw-light d-inline"
                }
            },
            display: {
                tag: "SPAN",
                attrs: {
                    class: "text-muted fw-light d-inline"
                }
            }
        }
        static container = {
            tag: "H1",
            attrs: {
                class: "m-0 p-0 d-inline-block"
            }
        }
    }

    function getObjVal(obj, strPath) {
        const target = obj, path = strPath, arrPath = path.split(".");
        return traverse(target, arrPath);
        function traverse(obj, path) {
            const arrPath = path, target = obj;
            let prop = arrPath.shift();
            if (!target.hasOwnProperty(prop)) return undefined;
            return (arrPath.length != 0) ? traverse(target[prop], arrPath) : target[prop];
        }
    }

    window.DEV.Breadcrumbs = class Breadcrumbs {
        _selector;
        _targets;
        _data;
        _uOpt;
        _sOpt = BreadcrumbsDefaultSettings;

        constructor(selector, data, opt) {
            const slctr = selector,
                mData = data,
                options = opt ?? {};

            const targets = document.querySelectorAll(slctr);
            if (targets.length == 0) throw "No elements found. Invalid Selector.";

            this._targets = targets;
            this._uOpt = options;
            this._data = mData;

            const uOpt = this._uOpt;
            const sOpt = this._sOpt;
            const suOpt = this._combineOpt(uOpt, sOpt);

            this._setupNodes(suOpt);

        }

        _setupNodes(options) {
            const opt = options,
                data = this._data,
                cOpt = this._getContainerOption(opt),
                separatorOpt = this._getSeparatorOpt(opt);
            const { _targets: targets } = this;
            const separateContainer = this._getOption("separateContainer", opt);

            const cNode = this._createContainerNode(cOpt),
                eNodes = this._createEntries(data, opt),
                separator = NodeTreeBuilder.build(separatorOpt);
            this._structureNodes(cNode, eNodes, separator);

            if (separateContainer) {
                targets.forEach(node => node.append(cNode.cloneNode(true)));
            } else {
                targets.forEach((node) => {
                    NodeTreeBuilder._addAttrs(node, cOpt.attrs);
                    node.append(...cNode.cloneNode(true).children);
                });
            }

        }

        _structureNodes(containerNode, nodes, separator) {
            const container = containerNode,
                childNodes = nodes;
            const lastChildIndex = nodes.length - 1;
            childNodes.forEach((node, i) => {
                container.append(node);
                if (lastChildIndex === i) return;
                container.append(separator.cloneNode(true));
            });
        }

        _createContainerNode(containerOpt) {
            const cOpt = containerOpt;
            const node = NodeTreeBuilder.build(cOpt);
            return node;
        }

        _getContainerOption(suOpt) {
            const { _getOption } = this;
            const defAnduserOpt = suOpt, { sOpt: defOpt } = defAnduserOpt, { uOpt: usrOpt } = defAnduserOpt;
            const container = _getOption("container", usrOpt, defOpt);
            return container;
        }

        _createEntries(data, suOpt) {
            const entries = data,
                options = suOpt;


            const activeOpt = this._getActiveOption(options);
            const inactiveOpt = this._getInactiveOption(options);
            const displayOpt = this._getDisplayOption(options);

            let nodes = [];

            for (const [i, val] of Object.entries(entries)) {
                const valType = typeof val;
                const isLast = Number(i) === entries.length - 1,
                    hasHref = !(valType === "string" || (Array.isArray(val) && val.length === 1));

                let node;

                if (isLast) {
                    node = NodeTreeBuilder.build(activeOpt);
                } else {
                    if (hasHref) {
                        node = NodeTreeBuilder.build(inactiveOpt);
                    } else {
                        node = NodeTreeBuilder.build(displayOpt);
                    }
                }

                node.textContent = Array.isArray(val) ? val[0] : val;
                if (hasHref) node.setAttribute("href", val[1]);
                nodes.push(node);
            }
            return nodes;
        }

        _getActiveOption(suOpt) {
            const { _getOption } = this;
            const defAnduserOpt = suOpt, { sOpt: defOpt } = defAnduserOpt, { uOpt: usrOpt } = defAnduserOpt;
            const active = _getOption("entries.active", usrOpt, defOpt);
            return active;
        }

        _getInactiveOption(suOpt) {
            const { _getOption } = this;
            const defAnduserOpt = suOpt, { sOpt: defOpt } = defAnduserOpt, { uOpt: usrOpt } = defAnduserOpt;
            const inactive = _getOption("entries.inactive", usrOpt, defOpt);
            return inactive;
        }

        _getDisplayOption(suOpt) {
            const { _getOption } = this;
            const defAnduserOpt = suOpt,
                { sOpt: defOpt } = defAnduserOpt, { uOpt: usrOpt } = defAnduserOpt;
            const display = _getOption("entries.display", usrOpt, defOpt);
            return display;
        }

        _getSeparatorOpt(suOpt) {
            const { _getOption } = this;
            const defAnduserOpt = suOpt,
                { sOpt: defOpt } = defAnduserOpt, { uOpt: usrOpt } = defAnduserOpt;
            const separator = _getOption("separator", usrOpt, defOpt);
            return separator;
        }



        _getOption(path, objsrc, defObjsrc) {
            let src, def;
            if (defObjsrc) {
                src = objsrc;
                def = defObjsrc;
            } else {
                src = objsrc.uOpt;
                def = objsrc.sOpt;
            }
            const data = getObjVal(src, path) ?? getObjVal(def, path);
            return data;
        }

        _combineOpt(usrOpt, defOpt) {
            const combinedOption = {
                uOpt: usrOpt,
                sOpt: defOpt
            }
            return combinedOption;
        }

    }

})(window);