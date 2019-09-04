var app = app || {};
app.postcondition = app.postcondition || {};
app.postcondition.constants = app.postcondition.constants || {};

app.postcondition.constants = (function () {
    const deny = 2;
    const modify = 1;
    const duplicate = 0;
    const unassignConstant = null;
    
    return {
        Deny: deny,
        Modify: modify,
        Duplicate: duplicate,
        UnassignConstant: unassignConstant,
    };
})();